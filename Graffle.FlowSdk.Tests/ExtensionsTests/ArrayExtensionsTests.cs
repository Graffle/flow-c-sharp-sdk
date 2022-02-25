using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Graffle.FlowSdk.Tests.ExtensionsTests
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        [TestMethod]
        public void ToValueData_ReturnsList()
        {
            var intType = new Int16Type(123);
            var ufix64Type = new UFix64Type(2345.6m);
            var arrayType = new ArrayType(new List<FlowValueType>() { intType, ufix64Type });

            var result = arrayType.ToValueData();
            Assert.IsInstanceOfType(result, typeof(List<object>));

            var list = result as List<object>;
            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(intType.Data, list[0]);
            Assert.AreEqual(ufix64Type.Data, list[1]);
        }
    }
}