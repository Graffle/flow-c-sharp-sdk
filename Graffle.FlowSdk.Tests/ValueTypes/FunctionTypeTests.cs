using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Graffle.FlowSdk.Types;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class FunctionTypeTests
    {
        [TestMethod]
        public void FromJson_DeserializesCorrectly()
        {
            var json = @"{""type"":""Function"",""value"":{""functionType"":{""kind"":""Function"",""typeID"":""(():Void)"",""parameters"":[],""return"":{""kind"":""Void""}}}}";

            var res = FunctionType.FromJson(json);

            var data = res?.Data as FunctionTypeDefinition;
            Assert.IsNotNull(data);

            Assert.AreEqual("Function", res.Type);
            Assert.AreEqual("Function", data.Kind);
            Assert.AreEqual("(():Void)", data.TypeId);
            Assert.AreEqual(0, data.Parameters.Count);

            var ret = data.Return as SimpleTypeDefinition;
            Assert.IsNotNull(ret);
            Assert.AreEqual("Void", ret.Kind);
        }

        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectJson()
        {
            var json = @"{""type"":""Function"",""value"":{""functionType"":{""kind"":""Function"",""typeID"":""(():Void)"",""parameters"":[],""return"":{""kind"":""Void""}}}}";
            var res = FunctionType.FromJson(json);

            var resJson = res.AsJsonCadenceDataFormat();
            Assert.AreEqual(json, resJson);

            //make sure its real json i guess
            var parsed = JsonDocument.Parse(resJson);
        }

        [TestMethod]
        public void FlattenTest()
        {
            var json = @"{""type"":""Function"",""value"":{""functionType"":{""kind"":""Function"",""typeID"":""(():Void)"",""parameters"":[],""return"":{""kind"":""Void""}}}}";
            var res = FunctionType.FromJson(json);

            var flat = res.Flatten() as Dictionary<string, dynamic>;
            Assert.IsTrue(flat.ContainsKey("functionType"));

            var functionType = flat["functionType"] as Dictionary<string, dynamic>;
            Assert.IsNotNull(functionType);

            Assert.AreEqual("Function", functionType["kind"]);
            Assert.AreEqual("(():Void)", functionType["typeID"]);

            var parameters = functionType["parameters"] as List<dynamic>;
            Assert.IsNotNull(parameters);
            Assert.AreEqual(0, parameters.Count);

            var ret = functionType["return"] as Dictionary<string, dynamic>;
            Assert.IsNotNull(ret);
            Assert.AreEqual("Void", ret["kind"]);
        }
    }
}