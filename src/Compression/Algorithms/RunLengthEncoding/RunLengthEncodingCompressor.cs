﻿using Compression.Algorithms.Huffman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression.Algorithms.RunLengthEncoding
{
    public class RunLengthEncodingCompressor : ICompressor
    {
        private const byte MAX_PACKAGE_SIZE = 255; 
        public CompressedFile Compress(byte [] file)
        {
            List<byte> newFile = new List<byte>();

            byte runValue = file[0];
            byte runCount = 1;
            for (int position = 1; position < file.Length; position++)
            {
                byte currentByte = file[position];
                if(runValue == currentByte)
                {
                    if(runCount != MAX_PACKAGE_SIZE)
                        runCount++;
                    else
                    {
                        newFile.Add(runCount);
                        newFile.Add(runValue);
                        runCount = 1;
                    }
                }
                else
                {
                    newFile.Add(runCount);
                    newFile.Add(runValue);
                    runValue = currentByte;
                    runCount = 1;
                }
            }

            newFile.Add(runCount);
            newFile.Add(runValue);
            
            CompressedFile compressedFile = new CompressedFile(newFile.ToArray(), 0, null);
            
            return compressedFile;
        }

        public byte[] Decompress(CompressedFile compressedFile)
        {
            const int nextCountOffset = 2;
            const int runValueOffset = 1;

            byte[] compressedFileData = compressedFile.Data;

            List<byte> file = new List<byte>();
            for (int position = 0; position < compressedFileData.Length; position += nextCountOffset)
            {
                byte runCount = compressedFileData[position];
                byte runValue = compressedFileData[position + runValueOffset];

                for (int count = 0; count < runCount; count++)
                    file.Add(runValue);
            }

            return file.ToArray();
        }
    }
}
