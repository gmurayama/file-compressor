using Compression.Algorithms.Huffman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression.Algorithms
{
    public interface ICompressor
    {
        CompressedFile Compress(byte[] file);
        byte[] Decompress(CompressedFile compressedFile);
    }
}
