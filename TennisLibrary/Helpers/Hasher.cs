using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TennisLibrary.Helpers
{
    public class Hasher
    {
        public static async Task<string?> CreateHashStringAsync(string inputString)
        {
            string result = "";
            result = BitConverter.ToString(await CreateHashByteArrayAsync(inputString));

            return result;
        }

        public static async Task<Byte[]> CreateHashByteArrayAsync(string inputString)
        {
            Byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(inputString);
            Byte[] tmpHash = SHA256.Create().ComputeHash(tmpSource);

            return tmpHash;
        }

        public static async Task<bool> CompareByteArrayHashAsync(Byte[] bArray1, Byte[] bArray2)
        {
            if (bArray1.Length == bArray2.Length)
            {
                int i = 0;
                while (i < bArray1.Length)
                {
                    if (bArray1[i] != bArray2[i]) return false;
                }
                return true;
            }
            return false;
        }
    }
}
