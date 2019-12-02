using Compression.DataStructures;
using System.Collections;
using System.Collections.Generic;

namespace Compression.Algorithms.Huffman
{
    public class CompressedFile
    {
        public CompressedFile(byte[] data, int extraBits, Node<byte>[] queue)
        {
            Data = data;
            ExtraBits = extraBits;
            Queue = queue;
        }

        public int ExtraBits { get; private set; }

        public byte[] Data { get; private set; }

        public Node<byte>[] Queue { get; private set; }

        public string Name { get; set; }

        public BitArray BitsTrimmed()
        {
            var byteAsBits = new BitArray(Data);
            var bits = new bool[byteAsBits.Length - ExtraBits];

            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = byteAsBits[i];
            }

            return new BitArray(bits);
        }
    }
}