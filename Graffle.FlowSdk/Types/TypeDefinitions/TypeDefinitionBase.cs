using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public abstract class TypeDefinitionBase
    {
        /// <summary>
        /// Flattens the Type Definition and all of its properties into a dictionary of primitive types
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, dynamic> Flatten();

        public abstract string AsJsonCadenceDataFormat();
    }
}