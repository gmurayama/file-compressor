using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression.Algorithms.RunLengthEncoding
{
    class RunLengthEncodingCompressor
    {
        private const byte MAX_PACKAGE_SIZE = 255; 
        public byte[] CompressFile()
        {
            List<byte> newFile = new List<byte>();

            byte[] file = new byte[] { 1, 2, 2, 2, 1 };

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

            return newFile.ToArray();
        }

        public byte[] Decompress(byte[] compressedFile)
        {
            List<byte> file = new List<byte>();
            for (int position = 0; position < compressedFile.Length; position += 2)
            {
                byte runCount = compressedFile[position];
                byte runValue = compressedFile[position + 1];

                for (int count = 0; count < runCount; count++)
                    file.Add(runValue);
            }

            return file.ToArray();
        }
    }
}
