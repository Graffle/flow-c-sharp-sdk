using Graffle.FlowSdk.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Graffle.FlowSdk.Tests.CryptoTests
{
    [TestClass]
    public class ECDSATests
    {
        [TestMethod]
        public void AlgorithmCurveNameFromEnum_ECDSAP256_ReturnsP256()
        {
            var result = ECDSA.AlgorithmCurveNameFromEnum(SignatureAlgorithm.ECDSA_P256);
            Assert.AreEqual("P-256", result);
        }

        [TestMethod]
        public void AlgorithmCurveNameFromEnum_ECDSA_secp256k1_secp256k1()
        {
            var result = ECDSA.AlgorithmCurveNameFromEnum(SignatureAlgorithm.ECDSA_secp256k1);
            Assert.AreEqual("secp256k1", result);
        }

        [TestMethod]
        public void AlgorithmCurveNameFromEnum_InvalidAlgorithm_ThrowsException()
        {
            Assert.ThrowsException<Exception>(() => ECDSA.AlgorithmCurveNameFromEnum(default(SignatureAlgorithm)));
        }
    }
}