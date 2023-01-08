using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace Console.Security
{
    public class HashTest
    {
        public static void CalculateHash()
        {
            HashAlgorithm alg = SHA256.Create();
            string password = "123";
            string seed = "YCpPYNEtx1vinEs1%4hv";
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            //byte[] passwordBytes2 = System.Convert.FromBase64String("sJulX1PoHHMr6DCP5j/Xrtnc4PxMhwpQ74zDVUwfMek=");
            byte[] seedBytes = Encoding.UTF8.GetBytes(seed);
            //
            var bytes = new byte[passwordBytes.Length + seedBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, bytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, bytes, passwordBytes.Length, passwordBytes.Length);
            //
            byte[] resultBytes = alg.ComputeHash(bytes);
            var result = System.Convert.ToBase64String(resultBytes);
            System.Console.WriteLine(result);
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
