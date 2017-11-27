using System;
using System.Linq;
using System.Security.Cryptography;

namespace opbeat.Core.Tests
{
    public static class Sha1Generator
    {
        private static readonly Random Random = new Random();
        private static readonly SHA1 Hasher = SHA1.Create();

        public static string RandomString()
        {
            var length = 40;

            var junkData = Enumerable.Range(0, length).Select(_ => (byte) Random.Next(50, 100)).ToArray();

            var hash = Hasher.ComputeHash(junkData);

            var sha1 = string.Join("", hash.Select(x => x.ToString("X2")));

            return sha1;
        }
    }
}