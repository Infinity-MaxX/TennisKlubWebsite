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
        public static string CreateHashString(string inputString)
        {
            string result = "";
            result = BitConverter.ToString(CreateHashByteArray(inputString));

            return result;
        }

        public static Byte[] CreateHashByteArray(string inputString)
        {
            Byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(inputString);
            Byte[] tmpHash = SHA256.Create().ComputeHash(tmpSource);

            return tmpHash;
        }

        public static bool CompareByteArrayHash(Byte[] bArray1, Byte[] bArray2)
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
