
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Protobuf;

namespace Graffle.FlowSdk {
    public static class StringExtensions {
        public static byte[] HexToByteArray(this string hexString)
        {
            var characterCount = hexString.Length;
            var byteArray = new byte[characterCount / 2];
            for (int i = 0; i < characterCount; i += 2) {
                byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return byteArray;
        }

        public static string ByteArrayToHex(this byte[] byteArray)
        {
            var hexString = new StringBuilder(byteArray.Length * 2);
            foreach (var b in byteArray) {
                hexString.AppendFormat("{0:x2}", b);
            }
            return hexString.ToString();
        }

        public static Google.Protobuf.ByteString HexToByteString(this string hexString)
        {
            var addressByte = hexString.HexToByteArray();
            return Google.Protobuf.ByteString.CopyFrom(addressByte);
        }

        public static string ByteStringToHex(this Google.Protobuf.ByteString byteString)
        {
            return byteString.ToByteArray().ByteArrayToHex();
        }

        public static string RemoveHexPrefix(this string hex)
        {
            return hex.Substring(hex.StartsWith("0x") ? 2 : 0);            
        }

        public static byte[] HexToBytes(this string hex)
        {
            hex = RemoveHexPrefix(hex);

            if (IsHexString(hex))
            {
                return Enumerable.Range(0, hex.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                    .ToArray();
            }

            throw new Exception("Invalid hex string.");      
        }        

        public static bool IsHexString(string str)
        {
            try
            {
                if (str.Length == 0)
                    return false;

                str = RemoveHexPrefix(str);

                var regex = new Regex(@"^[0-9a-f]+$");
                return regex.IsMatch(str) && str.Length % 2 == 0;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to determine if string is hex.", exception);
            }            
        }

        public static string StringToHex(this string str)
        {
            try
            {
                return ByteArrayToHex(Encoding.UTF8.GetBytes(str));
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to convert string to hex.", exception);
            }            
        }

        public static ByteString StringToByteString(this string str)
        {
            return ByteString.CopyFromUtf8(str);     
        }
        
        public static ByteString ByteArrayToByteString(this byte[] bytes)
        {
            return ByteString.CopyFrom(bytes);           
        }
    }
}