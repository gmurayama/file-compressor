using System;
using System.Collections;
using System.Linq;
using System.Text;
using Compression.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Compression.Tests
{
    [TestClass]
    public class HuffmanTests
    {        
        [TestMethod]
        public void TestCompression()
        {
            string content = "dabdccadadad";
            byte[] file = Encoding.UTF8.GetBytes(content);

            var huffman = new Huffman();

            huffman.Compress(file);
            var compressedFile = huffman.CompressedData;
            var bits = new BitArray(compressedFile);

            Assert.AreEqual("0111000101101110110110", bits.PrintArray());
        }

        [TestMethod]
        public void TestDecompression()
        {
            string content = "The quick Brown Fox jumps over the Lazy Dog";
            byte[] file = Encoding.UTF8.GetBytes(content);

            var huffman = new Huffman();

            huffman.Compress(file);
            var compressed = huffman.CompressedData;
            var decompressed = huffman.Decompress(compressed);

            string decompressedContent = Encoding.UTF8.GetString(decompressed);

            Assert.AreEqual(content, decompressedContent);
        }
    }
}
