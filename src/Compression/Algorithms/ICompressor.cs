using Compression.Algorithms.Huffman;

namespace Compression.Algorithms
{
    public interface ICompressor
    {
        CompressedFile Compress(byte[] file);
        byte[] Decompress(CompressedFile compressedFile);

        decimal Percentage { get; }
    }
}
