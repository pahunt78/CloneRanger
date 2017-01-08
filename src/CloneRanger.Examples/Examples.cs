using System.Collections.Generic;

namespace CloneRanger.Examples
{
    public class Examples
    {
        public AnyClass CloneAnObject(AnyClass original)
        {            
            var cloner = new Cloner();
            return cloner.Clone(original);
        }

        public AnyClass CloneAnObjectUsingExtensionMethod(AnyClass original)
        {
            return original.Clone();
        }

        public List<string> CloneAnObjectUsingExtensionMethod(List<string> original)
        {
            return original.Clone();
        }
    }
}