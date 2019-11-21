using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    
    public enum CompressionType
    {
        HUFFMAN, RLE,
        TOTAL
    }
    
    public class HuffNode
    {
         bits;
         value;
    }
    public class HuffTree
    {
        List<HuffNode> nodes;
        public HuffTree(byte [] byteArray)
        {
            
        }

        public byte [] ToByteArray()
        {
            foreach(HuffNode node in nodes)
            {
                BitConverter.
            }
            return 
        }
    }
    public class FileHeader
    {
        byte[] Identifier = new byte[4]; /* AED2*/
        byte[] FileExtension = new byte[6]; /* Original file extension, without dot */
        byte CompressionMethod; /* H - Huffman, R- RLE */
        byte n;
        int TreeSize; /* 0 if RLE, size of Huffman Tree Structure */
        HuffTree huffTree;

        public void Load(ref byte [] file)
        {
            Identifier = GetBytes(ref file, 0, 4 );
            FileExtension = GetBytes(ref file, 4, 6);
            CompressionMethod = file[10];
            n = file[11];
            TreeSize = BitConverter.ToInt16(file, 12);
            if (TreeSize == 0)
                huffTree = null;
            huffTree = new HuffTree(GetBytes(ref file, 14, TreeSize));
        }

        public byte [] Create()
        {
            List<byte> byteList = new List<byte>();
            byteList.AddRange(Identifier);
            byteList.AddRange(FileExtension);
            byteList.Add(CompressionMethod);
            byteList.Add(n);
            byteList.AddRange(BitConverter.GetBytes(TreeSize));
            if (huffTree != null)
                byteList.AddRange(huffTree.ToByteArray());

            return byteList.ToArray();
        }

        public byte[] GetBytes(ref byte[] file,long initial, long nBytes)
        {
            byte[] str = new byte[nBytes];

            for(long i = 0; i < nBytes; i++)
                str[i] = file[initial + i];

            return str;
        }
        
    }

    static class Compressor
    {
        public static string Extension = ".AED2";
        public static byte [] Compress(byte [] data, decimal size, CompressionType type)
        {
            return data;
        }

        public static byte[] Decompress(byte[] data)
        {
            return data;
        }
    }

    static class Decompressor
    {
        static FileHeader header;

        public static byte [] Decompress(byte[] data)
        {
            header.Load(ref data);
            return data;
        }
    }



}
