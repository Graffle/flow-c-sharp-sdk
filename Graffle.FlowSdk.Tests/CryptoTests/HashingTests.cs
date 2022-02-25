using Graffle.FlowSdk.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Graffle.FlowSdk.Tests.CryptoTests
{
    [TestClass]
    public class HashingTests
    {
        [TestMethod]
        public void CalculateHash_InvalidAlgorithm_ThrowsException()
        {
            Assert.ThrowsException<Exception>(() => Hashing.CalculateHash(null, default(Graffle.FlowSdk.Cryptography.HashAlgorithm)));
        }

        [TestMethod]
        public void CalculateHash_SHA2_256_ReturnsValidHash()
        {
            var stringToHash = "hello world";
            var bytesToHash = System.Text.Encoding.UTF8.GetBytes(stringToHash);
            using var sha = SHA256.Create();
            var expectedValue = sha.ComputeHash(bytesToHash);

            var result = Hashing.CalculateHash(bytesToHash, Graffle.FlowSdk.Cryptography.HashAlgorithm.SHA2_256);
            Assert.IsTrue(expectedValue.SequenceEqual(result));
        }

        [TestMethod]
        public void CalculateHash_SHA3_256_ReturnsValidHash()
        {
            var stringToHash = "hello world";
            var bytesToHash = System.Text.Encoding.UTF8.GetBytes(stringToHash);

            var algo = new Sha3Digest(256);
            var expectedValue = new byte[algo.GetDigestSize()];
            algo.BlockUpdate(bytesToHash, 0, bytesToHash.Length);
            algo.DoFinal(expectedValue, 0);

            var result = Hashing.CalculateHash(bytesToHash, Graffle.FlowSdk.Cryptography.HashAlgorithm.SHA3_256);
            Assert.IsTrue(expectedValue.SequenceEqual(result));
        }
    }
}