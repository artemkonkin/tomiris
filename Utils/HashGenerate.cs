using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace tomiris.utils
{
    public class HashGenerate
    {
        public string Compute(string data)
        {
            string hash = "Empty HASH";

            try
            {
                hash = Generate(data);
                return hash;
            }
            catch
            {
                Debug.WriteLine("Error! ");
                return hash;
            }
            finally
            {
                if (String.IsNullOrWhiteSpace(data))
                {
                    Debug.WriteLine("String is NULL or empty.");
                }
            }
        }

        private static string Generate(string inputData)
        {
            ASCIIEncoding encoding = new();
            StringBuilder builder = new();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(encoding.GetBytes(inputData));
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2")); //беру первые два байта и добавляю в конец строки
                }
            }
            return builder.ToString();
        }
    }
}