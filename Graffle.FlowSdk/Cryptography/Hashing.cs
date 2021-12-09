using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Security.Cryptography;

namespace Graffle.FlowSdk.Cryptography {

    public static class Hashing {
        public static byte[] CalculateHash(byte[] bytes, HashAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case HashAlgorithm.SHA2_256:
                    return HashSha2_256(bytes);
                case HashAlgorithm.SHA3_256:
                    return HashSha3(bytes, 256);
            }
            throw new Exception("Hash algorithm not supported");
        }

        public static byte[] HashSha2_256(byte[] bytes)
        {
            using (var sha256Hash = SHA256.Create())
                return sha256Hash.ComputeHash(bytes);
        }

        public static byte[] HashSha3(byte[] bytes, int bitLength)
        {
            var hashAlgorithm = new Sha3Digest(bitLength);
            var result = new byte[hashAlgorithm.GetDigestSize()];
            hashAlgorithm.BlockUpdate(bytes, 0, bytes.Length);
            hashAlgorithm.DoFinal(result, 0);
            return result;
        }
    }
}