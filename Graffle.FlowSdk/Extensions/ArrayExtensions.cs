using System.Collections.Generic;
using Graffle.FlowSdk.Types;

namespace Graffle.FlowSdk
{
    public static class ArrayExtensions
    {
        public static dynamic ToValueData(this ArrayType x)
        {
            var result = new List<dynamic>();
            foreach (var item in x.Data)
            {
                result.Add(((dynamic)item).Data);
            }

            return result;
        }
    }
}