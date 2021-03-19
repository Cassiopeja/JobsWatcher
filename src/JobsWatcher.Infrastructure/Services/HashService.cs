using System.Security.Cryptography;
using System.Text;
using JobsWatcher.Core.Interfaces;

namespace JobsWatcher.Infrastructure.Services
{
    public class HashService : IHashService
    {
        public byte[] GetHash(string inputString)
        {
            using HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public string GetHashString(string inputString)
        {
            var sb = new StringBuilder();
            foreach (var b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}