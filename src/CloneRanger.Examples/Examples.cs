using System.Collections.Generic;

namespace CloneRanger.Examples
{
    public class Examples
    {
        public SimpleClass CloneAnObject(SimpleClass original)
        {            
            var cloner = new Cloner();
            return cloner.Clone(original);
        }        

        public SimpleClass CloneAnObjectUsingExtensionMethod(SimpleClass original)
        {
            return original.Clone();
        }

        public NoParameterlessConstructor CloneAnObjectWithNoParameterlessConstructor(NoParameterlessConstructor original)
        {
            var cloner = new Cloner();
            return cloner.Clone(original, () => new NoParameterlessConstructor(0));
        }

        public NoParameterlessConstructor CloneAnObjectWithNoParameterlessConstructorUsingExtensionMethod(NoParameterlessConstructor original)
        {
            return original.Clone(() => new NoParameterlessConstructor(0));
        }
    }
}