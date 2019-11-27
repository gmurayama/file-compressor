using System.Collections;

namespace Compression
{
    public static class BitArrayExtensions
    {
        public static BitArray Add(this BitArray array, bool value)
        {
            bool[] bools = new bool[array.Length + 1];
            array.CopyTo(bools, 0);
            bools[bools.Length - 1] = value;
            return new BitArray(bools);
        }

        public static BitArray Add(this BitArray array, BitArray value)
        {
            bool[] bools = new bool[array.Length + value.Length];
            array.CopyTo(bools, 0);
            value.CopyTo(bools, array.Length);
            return new BitArray(bools);
        }  

        public static bool AreEqual(this BitArray array, BitArray value)
        {
            if (array.Length != value.Length)
                return false;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != value[i])
                    return false;
            }

            return true;
        }

        public static string PrintArray(this BitArray array)
        {
            string code = "";

            for (int i = 0; i < array.Length; i++)
            {
                code += array[i] ? "1" : "0";    
            }

            return code;
        }
    }
}
