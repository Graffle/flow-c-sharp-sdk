using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using System;
using System.Linq;

namespace Graffle.FlowSdk.Cryptography {
    public static class ECDSA {
        public static string AlgorithmCurveNameFromEnum(SignatureAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case SignatureAlgorithm.ECDSA_P256:
                    return "P-256";
                case SignatureAlgorithm.ECDSA_secp256k1:
                    return "secp256k1";
            }

            throw new Exception("Invalid signature algorithm");
        }

        public static ECPrivateKeyParameters GeneratePrivateKeyFromHex(string privateKeyAsHex, SignatureAlgorithm algorithm)
        {
            var name = AlgorithmCurveNameFromEnum(algorithm);
            var curve = ECNamedCurveTable.GetByName(name);
            var domainParameters = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
            return new ECPrivateKeyParameters(new BigInteger(privateKeyAsHex, 16), domainParameters);
        }

        public static AsymmetricCipherKeyPair AsymmetricCipherKeyPairFromPrivateKey(string privateKeyAsHex, SignatureAlgorithm algorithm)
        {
            var privateKey = GeneratePrivateKeyFromHex(privateKeyAsHex, algorithm);
            var privateKeyArray = privateKey.D.ToByteArrayUnsigned();

            var privateKeyAsInt = new BigInteger(+1, privateKeyArray);

            var name = AlgorithmCurveNameFromEnum(algorithm);
            var curve = ECNamedCurveTable.GetByName(name);
            var domainParameters = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
            ECPoint eCPoint = curve.G.Multiply(privateKeyAsInt).Normalize();

            var publicKey = new ECPublicKeyParameters(eCPoint, domainParameters);

            return new AsymmetricCipherKeyPair(publicKey, privateKey);
        }

        public static string PublicKeyToHex(AsymmetricCipherKeyPair keyPair)
        {
            var publicKey = keyPair.Public as ECPublicKeyParameters;
            var xCoord = publicKey.Q.XCoord.ToBigInteger().ToByteArrayUnsigned();
            var yCoord = publicKey.Q.YCoord.ToBigInteger().ToByteArrayUnsigned();
            return xCoord.Concat(yCoord).ToArray().ByteArrayToHex();
        }

        public static string DecodePrivateKeyToHex(AsymmetricCipherKeyPair keyPair)
        {
            var privateKey = keyPair.Private as ECPrivateKeyParameters;
            return privateKey.D.ToByteArrayUnsigned().ByteArrayToHex();
        }

        public static string DecodePublicKeyToHex(AsymmetricCipherKeyPair keyPair)
        {
            var publicKey = keyPair.Public as ECPublicKeyParameters;
            var publicKeyX = publicKey.Q.XCoord.ToBigInteger().ToByteArrayUnsigned();
            var publicKeyY = publicKey.Q.YCoord.ToBigInteger().ToByteArrayUnsigned();
            return publicKeyX.Concat(publicKeyY).ToArray().ByteArrayToHex();
        }

        public static AsymmetricCipherKeyPair GenerateKeyPair(SignatureAlgorithm signatureAlgorithm = SignatureAlgorithm.ECDSA_P256)
        {
            var curveName = AlgorithmCurveNameFromEnum(signatureAlgorithm);

            var curve = ECNamedCurveTable.GetByName(curveName);
            var domainParamaters = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());

            var random = new SecureRandom();
            var keyParams = new ECKeyGenerationParameters(domainParamaters, random);

            var generator = new ECKeyPairGenerator("ECDSA");
            generator.Init(keyParams);
            var key = generator.GenerateKeyPair();

            if (DecodePublicKeyToHex(key).Length != 128)
                return GenerateKeyPair(signatureAlgorithm);

            return key;
        }
        
        public static IMessageSigner CreateSigner(string privateKeyHex, SignatureAlgorithm signatureAlgorithm, HashAlgorithm hashAlgorithm)
        {
            var privateKeyParams = GeneratePrivateKeyFromHex(privateKeyHex, signatureAlgorithm);
            return new ECDSAMessageSigner(privateKeyParams, hashAlgorithm, signatureAlgorithm);
        }
    }
}
