using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class Word8Type : FlowValueType<uint>
    {
        public Word8Type(uint value) : base(value)
        {
        }

        public static Word8Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = uint.Parse(value.GetString());
            return new Word8Type(attempt); ;
        }

        public override string Type
            => WORD8_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }

    public class Word16Type : FlowValueType<uint>
    {
        public Word16Type(uint value) : base(value)
        {
        }

        public static Word16Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = uint.Parse(value.GetString());
            return new Word16Type(attempt); ;
        }

        public override string Type
            => WORD16_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }

    public class Word32Type : FlowValueType<uint>
    {
        public Word32Type(uint value) : base(value)
        {
        }

        public static Word32Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = uint.Parse(value.GetString());
            return new Word32Type(attempt); ;
        }

        public override string Type
            => WORD32_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }

    public class Word64Type : FlowValueType<UInt64>
    {
        public Word64Type(UInt64 value) : base(value)
        {
        }

        public static Word64Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = UInt64.Parse(value.GetString());
            return new Word64Type(attempt); ;
        }

        public override string Type
            => WORD64_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }

}