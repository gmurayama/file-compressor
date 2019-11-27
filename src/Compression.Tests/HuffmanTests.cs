using System.Collections;
using System.Text;
using Compression.Algorithms.Huffman;
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

            var huffman = new HuffmanCoding();

            var compressed = huffman.Compress(file);
            var bits = new BitArray(compressed.BitsTrimmed());

            Assert.AreEqual("0111000101101110110110", bits.PrintArray());
        }

        [TestMethod]
        public void TestDecompression()
        {
            string content = "The quick Brown Fox jumps over the Lazy Dog";
            byte[] file = Encoding.UTF8.GetBytes(content);

            var huffman = new HuffmanCoding();

            var compressed = huffman.Compress(file);
            var decompressed = huffman.Decompress(compressed);

            string decompressedContent = Encoding.UTF8.GetString(decompressed);

            Assert.AreEqual(content, decompressedContent);
        }
    }
}
