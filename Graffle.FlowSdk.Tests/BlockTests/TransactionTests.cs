using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.BlockTests
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        [Ignore]
        public async Task b() //todo what is this testing??
        {
            //https://flowscan.org/transaction/64a8781b0c6873d98ec30d5ef6ee296dcdddf93a8c2ec2e4378a6cfaea6b2631
            var fc = FlowClient.Create("access-001.mainnet16.nodes.onflow.org:9000"); //MAINNET16
            var txnId = "64a8781b0c6873d98ec30d5ef6ee296dcdddf93a8c2ec2e4378a6cfaea6b2631";

            var txn = await fc.GetTransactionResult(HashToByteString(txnId));

            var numEvents = txn.Events.Count();

            Assert.AreEqual(29625, numEvents);
        }

        private ByteString HashToByteString(string str)
        {
            var upper = str.ToUpperInvariant();
            var splitStr = Enumerable
                .Range(0, upper.Length / 2)
                .Select(i => upper.Substring(i * 2, 2)).ToList();
            var bytes = splitStr.Select(b => Convert.ToByte(b, 16)).ToArray();
            return ByteString.CopyFrom(bytes);
        }
    }
}