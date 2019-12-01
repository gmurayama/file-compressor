using Compression.DataStructures;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Compression.Algorithms.Huffman
{
    public class CompressedFile
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public CompressedFile(byte[] data, int extraBits, Node<byte>[] queue)
        {
            Data = data;
            ExtraBits = extraBits;
            Queue = queue;
        }

        public int ExtraBits { get; private set; }

        public byte[] Data { get; private set; }

        public Node<byte>[] Queue { get; private set; }

        public string OriginalFileName { get; set; }

        public bool isImage()
        {
            String fileExtension = System.IO.Path.GetExtension(OriginalFileName);
            if (ImageExtensions.Contains(fileExtension.ToUpperInvariant()))
                return true;

            return false;
        }

        public bool isHuffman()
        {
            if (Queue != null)
                return true;

            return false;
        }

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