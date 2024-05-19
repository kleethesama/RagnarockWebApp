using System.Security.Cryptography;
using System.Text;
using RagnarockWebApp.Interfaces;

namespace RagnarockWebApp.Models
{
    public class PwdHasher : IPwdHasher
    {
        public string GetHash(string userPwdInput) 
        {
            using var hasher = SHA512.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hasher.ComputeHash(Encoding.UTF8.GetBytes(userPwdInput));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
