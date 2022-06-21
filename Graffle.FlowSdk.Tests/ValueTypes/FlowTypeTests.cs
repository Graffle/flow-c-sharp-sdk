using System;
using System.Linq;
using Graffle.FlowSdk.Types;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class FlowTypeTests
    {
        [TestMethod]
        [DataRow("Resource")]
        [DataRow("Struct")]
        [DataRow("Event")]
        [DataRow("Contract")]
        [DataRow("StructInterface")]
        [DataRow("ResourceInterface")]
        [DataRow("ContractInterface")]
        public void CompositeTypeDefinition_ReturnsCompositeTypeDefinition(string kind)
        {
            var json = $"{{\"type\":\"Type\",\"value\":{{\"staticType\":{{\"kind\":\"{kind}\",\"typeID\":\"A.ff68241f0f4fd521.DrSeuss.NFT\",\"fields\":[{{\"id\":\"uuid\",\"type\":{{\"kind\":\"UInt64\"}}}},{{\"id\":\"id\",\"type\":{{\"kind\":\"UInt64\"}}}},{{\"id\":\"mintNumber\",\"type\":{{\"kind\":\"UInt32\"}}}},{{\"id\":\"contentCapability\",\"type\":{{\"kind\":\"Capability\",\"type\":\"\"}}}},{{\"id\":\"contentId\",\"type\":{{\"kind\":\"String\"}}}}],\"initializers\":[],\"type\":\"\"}}}}}}";

            var result = FlowType.FromJson(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("Type", result.Type);

            //break down the inner type definition and make sure it's all good
            var data = result.Data;
            Assert.IsInstanceOfType(data, typeof(CompositeTypeDefinition));

            var composite = data as CompositeTypeDefinition;
            Assert.AreEqual(kind, composite.Kind);
            Assert.AreEqual("A.ff68241f0f4fd521.DrSeuss.NFT", composite.TypeId);

            var fields = composite.Fields;
            Assert.IsNotNull(fields);
            Assert.AreEqual(5, fields.Count);

            Assert.AreEqual(string.Empty, composite.Type); //composite definitions do not have a type

            var initializers = composite.Initializers;
            Assert.IsNotNull(initializers);
            Assert.AreEqual(0, initializers.Count);
        }

        [TestMethod]
        public void CompositeTypeDefinition_WithInitializers_ReturnsCompositeTypeDefinition()
        {
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"Resource\",\"type\":\"\",\"typeID\":\"0x3.GreatContract.GreatNFT\",\"initializers\":[[{\"label\":\"foo\",\"id\":\"bar\",\"type\":{\"kind\":\"String\"}}]],\"fields\":[{\"id\":\"foo\",\"type\":{\"kind\":\"String\"}}]}}}";
            var result = FlowType.FromJson(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("Type", result.Type);

            //break down the inner type definition and make sure it's all good
            var data = result.Data;
            Assert.IsInstanceOfType(data, typeof(CompositeTypeDefinition));

            var composite = data as CompositeTypeDefinition;
            //just gonna test initializers here

            var initializers = composite.Initializers;
            Assert.AreEqual(1, initializers.Count); //initializers is an array of arrays

            var init = initializers.First();
            var parameters = init.Parameters;

            Assert.AreEqual(1, parameters.Count());
            var parameter = parameters.First();

            Assert.AreEqual("foo", parameter.Label);
            Assert.AreEqual("bar", parameter.Id);

            var paramType = parameter.Type;
            Assert.IsInstanceOfType(paramType, typeof(SimpleTypeDefinition));

            var simple = paramType as SimpleTypeDefinition;
            Assert.AreEqual("String", simple.Kind);
        }

        [TestMethod]
        [DataRow("UInt8")]
        [DataRow("Address")]
        [DataRow("String")]
        public void SimpleType_ReturnsSimpleType(string kind)
        {
            var json = $"{{\"type\":\"Type\",\"staticType\":{{\"kind\":\"{kind}\"}}}}";

            var result = FlowType.FromJson(json);
            Assert.IsNotNull(result);
            Assert.AreEqual("Type", result.Type);

            var data = result.Data;

            Assert.IsNotNull(data);
            Assert.IsInstanceOfType(data, typeof(SimpleTypeDefinition));

            var simple = data as SimpleTypeDefinition;

            Assert.AreEqual(kind, simple.Kind);
        }

        [TestMethod]
        public void DictionaryType_ReturnsDictionaryType()
        {
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"Dictionary\",\"key\":{\"kind\":\"String\"},\"value\":{\"kind\":\"UInt16\"}}}}";

            var result = FlowType.FromJson(json);
            Assert.IsNotNull(result);
            Assert.AreEqual("Type", result.Type);

            var data = result.Data;
            Assert.IsNotNull(data);
            Assert.IsInstanceOfType(data, typeof(DictionaryTypeDefinition));

            var dict = data as DictionaryTypeDefinition;

            var key = dict.Key;
            Assert.IsInstanceOfType(key, typeof(SimpleTypeDefinition));

            var simpleKey = key as SimpleTypeDefinition;
            Assert.AreEqual("String", simpleKey.Kind);

            var value = dict.Value;
            Assert.IsInstanceOfType(value, typeof(SimpleTypeDefinition));

            var simpleValue = value as SimpleTypeDefinition;

            Assert.AreEqual("UInt16", simpleValue.Kind);
        }

        [TestMethod]
        public void ReferenceType_ReturnsReferenceType()
        {
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"Reference\",\"authorized\":true,\"type\":{\"kind\":\"String\"}}}}";

            var result = FlowType.FromJson(json);

            Assert.IsNotNull(result);

            var data = result.Data;

            var reference = data as ReferenceTypeDefinition;

            Assert.AreEqual(true, reference.Authorized);
            Assert.AreEqual("Reference", reference.Kind);

            var type = reference.Type;

            Assert.IsInstanceOfType(type, typeof(SimpleTypeDefinition));
            var simple = type as SimpleTypeDefinition;

            Assert.AreEqual("String", simple.Kind);
        }

        [TestMethod]
        public void OptionalType_ReturnsOptionalType()
        {
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"Optional\",\"type\":{\"kind\":\"String\"}}}}";

            var result = FlowType.FromJson(json);

            var data = result.Data;

            var opt = data as OptionalTypeDefinition;
            Assert.IsNotNull(opt);

            Assert.AreEqual("Optional", opt.Kind);

            var type = opt.Type;

            var simple = type as SimpleTypeDefinition;
            Assert.AreEqual("String", simple.Kind);
        }

        [TestMethod]
        public void RestrictedType_ReturnsRestrictedType()
        {
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"Restriction\",\"typeID\":\"0x3.GreatContract.GreatNFT\",\"type\":{\"kind\":\"AnyResource\"},\"restrictions\":[{\"kind\":\"String\"}]}}}";

            var result = FlowType.FromJson(json);

            var data = result.Data;

            var restricted = data as RestrictedTypeDefinition;

            Assert.AreEqual(restricted.Kind, "Restriction");

            Assert.AreEqual("0x3.GreatContract.GreatNFT", restricted.TypeId);

            var type = restricted.Type;

            var simple = type as SimpleTypeDefinition;
            Assert.IsNotNull(simple);

            Assert.AreEqual("AnyResource", simple.Kind);

            Assert.AreEqual(1, restricted.Restrictions.Count());

            var actualRestriction = restricted.Restrictions[0];

            var simpleRestriction = actualRestriction as SimpleTypeDefinition;
            Assert.IsNotNull(simpleRestriction);

            Assert.AreEqual("String", simpleRestriction.Kind);
        }

        [TestMethod]
        public void RestrictedType_NonJsonObjectInRestrictionsArray_Succeeds()
        {
            //huge json here just verify no throw
            //todo: more accurate test
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"Resource\",\"typeID\":\"A.9d21537544d9123d.Momentables.NFT\",\"fields\":[{\"id\":\"uuid\",\"type\":{\"kind\":\"UInt64\"}},{\"id\":\"id\",\"type\":{\"kind\":\"UInt64\"}},{\"id\":\"momentableId\",\"type\":{\"kind\":\"String\"}},{\"id\":\"name\",\"type\":{\"kind\":\"String\"}},{\"id\":\"description\",\"type\":{\"kind\":\"String\"}},{\"id\":\"imageCID\",\"type\":{\"kind\":\"String\"}},{\"id\":\"directoryPath\",\"type\":{\"kind\":\"String\"}},{\"id\":\"traits\",\"type\":{\"kind\":\"Dictionary\",\"key\":{\"kind\":\"String\"},\"value\":{\"kind\":\"Dictionary\",\"key\":{\"kind\":\"String\"},\"value\":{\"kind\":\"String\"}}}},{\"id\":\"creator\",\"type\":{\"kind\":\"Struct\",\"typeID\":\"A.9d21537544d9123d.Momentables.Creator\",\"fields\":[{\"id\":\"creatorName\",\"type\":{\"kind\":\"String\"}},{\"id\":\"creatorWallet\",\"type\":{\"kind\":\"Capability\",\"type\":{\"kind\":\"Reference\",\"type\":{\"kind\":\"Restriction\",\"typeID\":\"AnyResource{A.f233dcee88fe0abe.FungibleToken.Receiver}\",\"type\":{\"kind\":\"AnyResource\"},\"restrictions\":[{\"kind\":\"ResourceInterface\",\"typeID\":\"A.f233dcee88fe0abe.FungibleToken.Receiver\",\"fields\":[{\"id\":\"uuid\",\"type\":{\"kind\":\"UInt64\"}}],\"initializers\":[],\"type\":\"\"}]},\"authorized\":false}}},{\"id\":\"creatorRoyalty\",\"type\":{\"kind\":\"UFix64\"}}],\"initializers\":[],\"type\":\"\"}},{\"id\":\"collaborators\",\"type\":{\"kind\":\"VariableSizedArray\",\"type\":{\"kind\":\"Struct\",\"typeID\":\"A.9d21537544d9123d.Momentables.Collaborator\",\"fields\":[{\"id\":\"collaboratorName\",\"type\":{\"kind\":\"String\"}},{\"id\":\"collaboratorWallet\",\"type\":{\"kind\":\"Capability\",\"type\":{\"kind\":\"Reference\",\"type\":{\"kind\":\"Restriction\",\"typeID\":\"AnyResource{A.f233dcee88fe0abe.FungibleToken.Receiver}\",\"type\":{\"kind\":\"AnyResource\"},\"restrictions\":[\"A.f233dcee88fe0abe.FungibleToken.Receiver\"]},\"authorized\":false}}},{\"id\":\"collaboratorRoyalty\",\"type\":{\"kind\":\"UFix64\"}}],\"initializers\":[],\"type\":\"\"}}},{\"id\":\"momentableCollectionDetails\",\"type\":{\"kind\":\"Dictionary\",\"key\":{\"kind\":\"String\"},\"value\":{\"kind\":\"String\"}}}],\"initializers\":[],\"type\":\"\"}}}";
            var x = FlowType.FromJson(json);

            var y = x.Data.Flatten();
            var resJson = System.Text.Json.JsonSerializer.Serialize(y);
        }

        [TestMethod]
        public void EnumType_ReturnsEnumType()
        {
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"Enum\",\"type\":{\"kind\":\"String\"},\"typeID\":\"0x3.GreatContract.GreatEnum\",\"initializers\":[],\"fields\":[{\"id\":\"rawValue\",\"type\":{\"kind\":\"String\"}}]}}}";

            var res = FlowType.FromJson(json);
            Assert.IsNotNull(res);

            var data = res.Data;
            Assert.IsInstanceOfType(data, typeof(EnumTypeDefinition));

            var enumTypeDef = data as EnumTypeDefinition;
            Assert.AreEqual("Enum", enumTypeDef.Kind);
            Assert.AreEqual("0x3.GreatContract.GreatEnum", enumTypeDef.TypeId);

            Assert.IsInstanceOfType(enumTypeDef.Type, typeof(SimpleTypeDefinition));
            var simpleEnumType = enumTypeDef.Type as SimpleTypeDefinition;
            Assert.AreEqual("String", simpleEnumType.Kind);

            var enumFields = enumTypeDef.Fields;
            Assert.AreEqual(1, enumFields.Count());

            var field = enumFields.First();
            Assert.AreEqual("rawValue", field.Id);
            Assert.IsInstanceOfType(field.Type, typeof(SimpleTypeDefinition));

            var fieldType = field.Type as SimpleTypeDefinition;
            Assert.AreEqual("String", fieldType.Kind);

            var initializers = enumTypeDef.Initializers;
            Assert.IsNotNull(initializers);
            Assert.AreEqual(0, initializers.Count());
        }

        [TestMethod]
        public void FunctionType_ReturnsFunctionType()
        {
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"Function\",\"typeID\":\"foo\",\"parameters\":[{\"label\":\"foo\",\"id\":\"bar\",\"type\":{\"kind\":\"String\"}}],\"return\":{\"kind\":\"String\"}}}}";

            var res = FlowType.FromJson(json);

            var data = res.Data;
            Assert.IsInstanceOfType(data, typeof(FunctionTypeDefinition));

            var function = data as FunctionTypeDefinition;
            Assert.AreEqual("Function", function.Kind);
            Assert.AreEqual("foo", function.TypeId);

            Assert.IsInstanceOfType(function.Return, typeof(SimpleTypeDefinition));
            var returnType = function.Return as SimpleTypeDefinition;
            Assert.AreEqual("String", returnType.Kind);

            Assert.AreEqual(1, function.Parameters.Count);
            var funcParam = function.Parameters.First();
            Assert.AreEqual("foo", funcParam.Label);
            Assert.AreEqual("bar", funcParam.Id);

            Assert.IsInstanceOfType(funcParam.Type, typeof(SimpleTypeDefinition));
            var funcParamType = funcParam.Type as SimpleTypeDefinition;
            Assert.AreEqual("String", funcParamType.Kind);
        }

        [TestMethod]
        public void ConstantSizedArray_ReturnsConstantSizedArray()
        {
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"ConstantSizedArray\",\"type\":{\"kind\":\"String\"},\"size\":3}}}";
            var res = FlowType.FromJson(json);

            var data = res.Data;
            Assert.IsInstanceOfType(data, typeof(ConstantSizedArrayTypeDefinition));

            var arr = data as ConstantSizedArrayTypeDefinition;
            Assert.AreEqual("ConstantSizedArray", arr.Kind);
            Assert.AreEqual((ulong)3, arr.Size);

            var arrType = arr.Type;
            Assert.IsInstanceOfType(arrType, typeof(SimpleTypeDefinition));

            var simple = arrType as SimpleTypeDefinition;
            Assert.AreEqual("String", simple.Kind);
        }

        [TestMethod]
        public void VariableSizedArray_ReturnsVariableSizedArray()
        {
            var json = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"VariableSizedArray\",\"type\":{\"kind\":\"String\"}}}}";
            var res = FlowType.FromJson(json);

            var data = res.Data;
            Assert.IsInstanceOfType(data, typeof(VariableSizedArrayTypeDefinition));

            var arr = data as VariableSizedArrayTypeDefinition;
            Assert.AreEqual("VariableSizedArray", arr.Kind);

            var arrType = arr.Type;
            Assert.IsInstanceOfType(arrType, typeof(SimpleTypeDefinition));

            var simple = arrType as SimpleTypeDefinition;
            Assert.AreEqual("String", simple.Kind);
        }
    }
}
