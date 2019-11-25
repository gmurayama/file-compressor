using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Compression
{
    public class Huffman
    {
        public byte[] CompressedData { get; private set; }

        public Node HuffmanTree { get; private set; }

        private Dictionary<byte, BitArray> HuffmanEncoding { get; set; } = new Dictionary<byte, BitArray>();

        public void Compress(byte[] file)
        {
            var frequency = file
                .GroupBy(b => b)
                .Select(g => new Node { Value = g.Key, Frequency = g.Count() })
                .OrderBy(b => b.Frequency)
                .ToList();

            HuffmanTree = BuildHuffmanTree(frequency);
            CompressedData = CompressBytes(file);
        }

        private byte[] CompressBytes(byte[] file)
        {
            BitArray bits = new BitArray(0);

            for (int i = 0; i < file.Length; i++)
            {
                var code = HuffmanEncoding[file[i]];
                var bools = new bool[bits.Count + code.Count];
                bits.CopyTo(bools, 0);
                code.CopyTo(bools, bits.Count);

                bits = new BitArray(bools);
            }

            var bytes = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(bytes, 0);
            return bytes;
        }

        private Node BuildHuffmanTree(List<Node> nodes)
        {
            if (nodes.Count > 1)
            {
                var nodeLeft = nodes[0];
                var nodeRight = nodes[1];

                nodes.RemoveRange(0, 2);

                var node = new Node
                {
                    Left = nodeLeft,
                    Right = nodeRight,
                    Frequency = nodeLeft.Frequency + nodeRight.Frequency
                };

                nodes.Add(node);
                nodes = nodes.OrderBy(n => n.Frequency).ToList();

                return BuildHuffmanTree(nodes);
            }
            else
            {
                var node = nodes.Single();
                BuildDictionary(node, new BitArray(0));
                return node;
            }
        }

        private void BuildDictionary(Node node, BitArray code)
        {
            if (node.Left == null && node.Right == null)
            {
                var byteValue = node.Value.Value;
                HuffmanEncoding.Add(byteValue, code);
            }

            var bools = new bool[code.Count + 1];
            code.CopyTo(bools, 0);

            if (node.Left != null)
            {
                bools[bools.Length - 1] = false;
                var newCode = new BitArray(bools);
                BuildDictionary(node.Left, newCode);
            }

            if (node.Right != null)
            {
                bools[bools.Length - 1] = true;
                var newCode = new BitArray(bools);
                BuildDictionary(node.Right, newCode);
            }
        }
    }

    public class Node
    {
        public int Frequency { get; set; }

        public byte? Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
    }
}
