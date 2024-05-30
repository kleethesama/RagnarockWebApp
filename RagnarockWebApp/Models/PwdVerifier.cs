using RagnarockWebApp.Interfaces;

namespace RagnarockWebApp.Models
{
    public class PwdVerifier
    {
        public IPwdHasher Hasher { get; set; }

        public PwdVerifier(IPwdHasher hasher)
        {
            Hasher = hasher;
        }

        public bool VerifyHash(string userPwdInput, string hash)
        {
            // Hash the input.
            string hashOfInput = Hasher.GetHash(userPwdInput);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}
