namespace CloneRanger
{
    public static class ClonerExtensionMethods
    {
        /// <summary>
        /// Clone the object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>        
        /// <returns>The cloned object.</returns>
        public static T Clone<T>(this T obj) where T: class
        {
            var cloner = new Cloner();
            return cloner.Clone(obj);
        }
    }
}