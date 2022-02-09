using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class FlowValueTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""Address"",""value"":""0x66d6f450e25a4e22""}";
            var flowValueType = FlowValueType.CreateFromCadence(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Address");
        }

        [TestMethod]
        public void CreateFromCadence_NullType_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FlowValueType.CreateFromCadence(null, "asdf"));
        }

        [TestMethod]
        public void CreateFromCadence_InvalidType_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => FlowValueType.CreateFromCadence("asdf", "asdf"));
        }

        [TestMethod]
        public void Create_NullType_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FlowValueType.Create(null, 1));
        }

        [TestMethod]
        public void Create_InvalidType_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => FlowValueType.Create("asdf", 1));
        }

        [TestMethod]
        public void Create_OptionalType_CreatesOptionalType()
        {
            var type = "Optional|Int16";

            var result = FlowValueType.Create(type, (short)123);

            //verify we got a OptionalType object back
            Assert.IsInstanceOfType(result, typeof(OptionalType));

            //get the data and verify it's int16
            var optional = result as OptionalType;
            var data = optional.Data;
            Assert.IsInstanceOfType(data, typeof(Int16Type));

            //verify value
            var int16Data = optional.Data as Int16Type;
            Assert.AreEqual(123, int16Data.Data);
        }

        [TestMethod]
        public void Create_ValidTypeInvalidValue_ThrowsInvalidOperationException()
        {
            var ex = Assert.ThrowsException<InvalidOperationException>(() => FlowValueType.Create("String", 123));

            Assert.IsNotNull(ex.InnerException);
        }
    }
}
