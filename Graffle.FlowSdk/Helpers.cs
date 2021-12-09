using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Graffle.FlowSdk {

    public static class Helpers {
        public static FlowJson LoadFlowJson(string path = null) {
            if(string.IsNullOrWhiteSpace(path)) {
                //default flow.json location when running tests
                path = "../../../flow.json";
            }

            var flowJsonFile = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<FlowJson>(flowJsonFile);
        }        

        public static byte[] Pad(string tag, int length, bool padLeft = true)
        {
            var bytes = Encoding.UTF8.GetBytes(tag);

            if (padLeft)
                return bytes.PadLeft(length);
            else
                return bytes.PadRight(length);
        }

        public static byte[] Pad(byte[] bytes, int length, bool padLeft = true)
        {
            if (padLeft)
                return bytes.PadLeft(length);
            else
                return bytes.PadRight(length);
        }

        public static byte[] PadLeft(this byte[] bytes, int length)
        {
            if (bytes.Length >= length)
                return bytes;

            var newArray = new byte[length];
            Array.Copy(bytes, 0, newArray, (newArray.Length - bytes.Length), bytes.Length);
            return newArray;
        }

        public static byte[] PadRight(this byte[] bytes, int length)
        {
            if (bytes.Length >= length)
                return bytes;

            Array.Resize(ref bytes, length);
            return bytes;
        }
    }
}