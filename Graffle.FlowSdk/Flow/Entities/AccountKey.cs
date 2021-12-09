using System.Collections.Generic;
using System.Linq;
using Graffle.FlowSdk.Cryptography;
using Graffle.FlowSdk;

namespace Flow.Entities {
    //extended properties of base proto classes
    public sealed partial class AccountKey {
        public string PrivateKey { get; set; }
        public IMessageSigner Signer { get; set; }
        public SignatureAlgorithm SignatureAlgorithm { get; set; }        
        public HashAlgorithm HashAlgorithm { get; set; }

        public static AccountKey GenerateRandomEcdsaKey(SignatureAlgorithm signatureAlgorithm, HashAlgorithm hashAlgorithm, uint weight = 1000)
        {
            var newKeys = ECDSA.GenerateKeyPair(signatureAlgorithm);
            var publicKey = ECDSA.DecodePublicKeyToHex(newKeys);
            var privateKey = ECDSA.DecodePrivateKeyToHex(newKeys);

            return new AccountKey
            {
                PrivateKey = privateKey,
                PublicKey = publicKey.HexToByteString(),
                SignatureAlgorithm = signatureAlgorithm,
                HashAlgorithm = hashAlgorithm,
                Weight = weight
            };
        }

        public static IList<AccountKey> UpdateFlowAccountKeys(IList<AccountKey> currentFlowAccountKeys, IList<AccountKey> updatedFlowAccountKeys)
        {
            foreach(var key in updatedFlowAccountKeys)
            {
                var currentKey = currentFlowAccountKeys.Where(w => w.PublicKey == key.PublicKey).FirstOrDefault();
                if(currentKey != null && !string.IsNullOrEmpty(currentKey.PrivateKey))
                {
                    key.PrivateKey = currentKey.PrivateKey;
                    key.Signer = ECDSA.CreateSigner(key.PrivateKey, key.SignatureAlgorithm, key.HashAlgorithm);
                }
            }

            return updatedFlowAccountKeys;
        }
    }
}