using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Graffle.FlowSdk.Tests.ExtensionsTests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void HexToByteArray_ReturnsByteArray()
        {
            var expected = "hello world";
            var hex = string.Join("", expected.Select(c => string.Format("{0:x2}", (int)c)));

            var result = hex.HexToByteArray();
            Assert.AreEqual(hex.Length / 2, result.Length);
            var strResult = System.Text.Encoding.UTF8.GetString(result);
            Assert.AreEqual(expected, strResult);
        }

        [TestMethod]
        public void ByteArrayToHex_ReturnsString()
        {
            var str = "hello world";
            var hex = string.Join("", str.Select(c => string.Format("{0:x2}", (int)c)));
            var bytes = System.Text.Encoding.UTF8.GetBytes(str);

            var result = bytes.ByteArrayToHex();
            Assert.AreEqual(hex, result);
        }

        [TestMethod]
        public void HexToByteString_ReturnsProtoBufByteString()
        {
            var str = "hello world";
            var hex = string.Join("", str.Select(c => string.Format("{0:x2}", (int)c)));

            var result = hex.HexToByteString();
            var strResult = result.ToStringUtf8();

            Assert.AreEqual(str, strResult);
        }

        [TestMethod]
        public void RemoveHexPrefix_ReturnsString()
        {
            var hex = "0xf4dc";
            var result = hex.RemoveHexPrefix();
            Assert.AreEqual(hex.Substring(2), result);
        }

        [TestMethod]
        [DataRow("696620796f75206172652072656164696e6720746869732068656c6c6f", true)]
        [DataRow("hello world", false)]
        public void IsHexString_ReturnsBool(string str, bool expectedValue)
        {
            var res = StringExtensions.IsHexString(str);
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void HexToBytes_StringNotHex_ThrowsException()
        {
            var ex = Assert.ThrowsException<Exception>(() => "hello".HexToBytes());
            Assert.IsTrue(!string.IsNullOrEmpty(ex.Message));
        }

        [TestMethod]
        public void HexToBytes_ValidHex_ReturnsByteArray()
        {
            var str = "hello world";
            var hex = string.Join("", str.Select(c => string.Format("{0:x2}", (int)c)));

            var res = hex.HexToBytes();
            var strRes = System.Text.Encoding.UTF8.GetString(res);
            Assert.AreEqual(str, strRes);
        }

        [TestMethod]
        public void StringToHex_NullString_ThrowsException()
        {
            string str = null;
            Assert.ThrowsException<Exception>(() => str.StringToHex());
        }

        [TestMethod]
        public void StringToHex_ReturnsString()
        {
            var str = "hello world";
            var hex = string.Join("", str.Select(c => string.Format("{0:x2}", (int)c)));

            var result = str.StringToHex();
            Assert.AreEqual(hex, result);
        }
    }
}