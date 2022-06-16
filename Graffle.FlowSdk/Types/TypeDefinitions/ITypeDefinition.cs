using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public interface ITypeDefinition
    {
        /// <summary>
        /// Flattens the Type Definition and all of its properties into a dictionary of primitive types
        /// </summary>
        /// <returns></returns>
        Dictionary<string, dynamic> Flatten();

        string AsJsonCadenceDataFormat();
    }
}