using Compression.DataStructures;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Compression.Algorithms.Huffman
{
    public class HuffmanCoding
    {
        private int position;

        private long fileLength;

        public HuffmanCoding()
        {
            position = 0;
        }

        public decimal Percentage { get => fileLength > 0 ? (decimal)position / fileLength * 100 : 0; }

        public CompressedFile Compress(byte[] file)
        {
            var frequency = file
                .GroupBy(b => b)
                .Select(g => new Node<byte> { Value = g.Key, Priority = g.Count() })
                .OrderBy(b => b.Priority)
                .ToList();

            var queue = CreatePriorityQueue(frequency);
            return Compress(file, queue);
        }

        private CompressedFile Compress(byte[] file, MinHeap<byte> queue)
        {
            fileLength = file.LongLength;

            BitArray bits = new BitArray(0);
            var queueAsArray = queue.ExportQueueAsArray();
            var huffmanTree = BuildHuffmanTree(queue);
            var dictionary = BuildCodingDictionary(huffmanTree);

            for (position = 0; position < file.Length; position++)
            {
                var code = dictionary[file[position]];
                bits = bits.Add(code);
            }

            position = 0;

            var bytes = new byte[(bits.Length - 1) / 8 + 1];
            int extraBits = (8 - bits.Length % 8) % 8;
            bits.CopyTo(bytes, 0);

            return new CompressedFile(bytes, extraBits, queueAsArray);
        }

        public byte[] Decompress(CompressedFile file)
        {
            var bits = new BitArray(file.Data);
            var decoded = new List<byte>();

            fileLength = bits.Length;

            var queue = CreatePriorityQueue(file.Queue);
            var huffmanTree = BuildHuffmanTree(queue);
            var dictionary = BuildEncodingDictionary(huffmanTree);

            for (int i = 0; i < bits.Length - file.ExtraBits; i = position + 1)
            {
                var code = new BitArray(0);
                byte byteCode = 0;

                for (position = i; position < bits.Length - file.ExtraBits; position++)
                {
                    code = code.Add(bits[position]);

                    var codeExists = dictionary.TryGetValue(code.PrintArray(), out byteCode);

                    if (codeExists)
                    {
                        break;
                    }
                }

                decoded.Add(byteCode);
            }

            return decoded.ToArray();
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

        private Dictionary<byte, BitArray> BuildCodingDictionary(Node<byte> huffmanTree)
        {
            return BuildCodingDictionary(huffmanTree, new BitArray(0), new Dictionary<byte, BitArray>());
        }

        private Dictionary<string, byte> BuildEncodingDictionary(Node<byte> huffmanTree)
        {
            return BuildEncodingDictionary(huffmanTree, new BitArray(0), new Dictionary<string, byte>());
        }

        private Dictionary<byte, BitArray> BuildCodingDictionary(Node<byte> huffmanTree, BitArray code, Dictionary<byte, BitArray> dictionary)
        {
            // Is Leaf
            if (huffmanTree.Left == null && huffmanTree.Right == null)
            {
                var value = huffmanTree.Value;
                dictionary.Add(value, code);
            }

            if (huffmanTree.Left != null)
            {
                var newCode = code.Add(false);
                BuildCodingDictionary(huffmanTree.Left, newCode, dictionary);
            }

            if (huffmanTree.Right != null)
            {
                var newCode = code.Add(true);
                BuildCodingDictionary(huffmanTree.Right, newCode, dictionary);
            }

            return dictionary;
        }

        private Dictionary<string, byte> BuildEncodingDictionary(Node<byte> huffmanTree, BitArray code, Dictionary<string, byte> dictionary)
        {
            // Is Leaf
            if (huffmanTree.Left == null && huffmanTree.Right == null)
            {
                var value = huffmanTree.Value;
                dictionary.Add(code.PrintArray(), value);
            }

            if (huffmanTree.Left != null)
            {
                var newCode = code.Add(false);
                BuildEncodingDictionary(huffmanTree.Left, newCode, dictionary);
            }

            if (huffmanTree.Right != null)
            {
                var newCode = code.Add(true);
                BuildEncodingDictionary(huffmanTree.Right, newCode, dictionary);
            }

            return dictionary;
        }
    }
}
