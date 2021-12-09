using System;

namespace Graffle.FlowSdk
{
    public static class DomainTag
    {
        public static byte[] MessageWithDomain(byte[] bytes, byte[] domain)
        {
            var newBytes = new byte[domain.Length + bytes.Length];
            Buffer.BlockCopy(domain, 0, newBytes, 0, domain.Length);
            Buffer.BlockCopy(bytes, 0, newBytes, domain.Length, bytes.Length);
            return newBytes;
        }

        public static byte[] AddUserDomainTag(byte[] bytes)
        {
            var userTag = Helpers.Pad("FLOW-V0.0-user", 32, false);
            return MessageWithDomain(bytes, userTag);
        }

        public static byte[] AddTransactionDomainTag(byte[] bytes)
        {
            var domainTag = Helpers.Pad("FLOW-V0.0-transaction", 32, false);
            return MessageWithDomain(bytes, domainTag);
        }
    }
}