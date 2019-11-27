using Compression.DataStructures;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Compression.Algorithms
{
    public class Huffman
    {
        public Huffman()
        {
            HuffmanEncoding = new Dictionary<byte, BitArray>();
        }

        public Huffman(Dictionary<byte, BitArray> encoding)
        {
            HuffmanEncoding = encoding;
        }

        public byte[] CompressedData { get; private set; }

        public int ExtraBits { get; private set; }

        private Dictionary<byte, BitArray> HuffmanEncoding { get; set; }

        public void Compress(byte[] file)
        {
            var frequency = file
                .GroupBy(b => b)
                .Select(g => new Node<byte> { Value = g.Key, Priority = g.Count() })
                .OrderBy(b => b.Priority);

            var queue = CreatePriorityQueue(frequency);
            var huffmanTree = BuildHuffmanTree(queue);
            BuildDictionary(huffmanTree);
            CompressedData = CompressBytes(file);
        }

        public byte[] Decompress(byte[] file)
        {
            var bits = new BitArray(file);
            var decoded = new List<byte>();
           
            for (int i = 0, j; i < bits.Length - ExtraBits; i = j + 1)
            {
                var code = new BitArray(0);
                byte byteCode = 0;

                for (j = i; j < bits.Length - ExtraBits; j++)
                {
                    code = code.Add(bits[j]);

                    var entry = HuffmanEncoding
                        .Select(d => new { d.Key, d.Value })
                        .FirstOrDefault(d => d.Value.AreEqual(code));

                    if (entry != null)
                    {
                        byteCode = entry.Key;
                        break;
                    }
                }

                decoded.Add(byteCode);
            }

            return decoded.ToArray();
        }

        private byte[] CompressBytes(byte[] file)
        {
            BitArray bits = new BitArray(0);

            for (int i = 0; i < file.Length; i++)
            {
                var code = HuffmanEncoding[file[i]];
                bits = bits.Add(code);
            }

            var bytes = new byte[(bits.Length - 1) / 8 + 1];
            ExtraBits = (8 - bits.Length % 8) % 8;
            bits.CopyTo(bytes, 0);
            return bytes;
        }

        private MinHeap<T> CreatePriorityQueue<T>(IEnumerable<Node<T>> nodes)
        {
            var heap = new MinHeap<T>();
            
            foreach (var node in nodes)
            {
                heap.Enqueue(node);
            }

            return heap;
        }

        private Node<T> BuildHuffmanTree<T>(MinHeap<T> queue)
        {
            while (queue.Size > 1)
            {
                var node1 = queue.Dequeue();
                var node2 = queue.Dequeue();

                var node = new Node<T> { Priority = node1.Priority + node2.Priority, Left = node1, Right = node2 };

                queue.Enqueue(node);
            }

            return queue.Dequeue();
        }

        private void BuildDictionary(Node<byte> huffmanTree)
        {
            BuildDictionary(huffmanTree, new BitArray(0));
        }

        private void BuildDictionary(Node<byte> huffmanTree, BitArray code)
        {
            // Is Leaf
            if (huffmanTree.Left == null && huffmanTree.Right == null)
            {
                var value = huffmanTree.Value;
                HuffmanEncoding.Add(value, code);
            }

            if (huffmanTree.Left != null)
            {
                var newCode = code.Add(false);
                BuildDictionary(huffmanTree.Left, newCode);
            }

            if (huffmanTree.Right != null)
            {
                var newCode = code.Add(true);
                BuildDictionary(huffmanTree.Right, newCode);
            }
        }
    }
}
