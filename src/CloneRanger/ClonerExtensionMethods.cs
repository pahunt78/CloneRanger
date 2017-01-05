namespace CloneRanger
{
    public static class ClonerExtensionMethods
    {
        public static T Clone<T>(this T obj) where T: class
        {
            var cloner = new Cloner();
            return cloner.Clone(obj);
        }
    }
}