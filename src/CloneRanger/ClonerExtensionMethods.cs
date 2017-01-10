using System;

namespace CloneRanger
{
    public static class ClonerExtensionMethods
    {
        /// <summary>
        /// Clone the object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>        
        /// <param name="obj">The object to clone.</param>
        /// <returns>The cloned object.</returns>
        public static T Clone<T>(this T obj) where T: class
        {
            var cloner = new Cloner();
            return cloner.Clone(obj);
        }

        /// <summary>
        /// Clone the object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to clone.</param>
        /// <param name="cloneConstruction">A function that constructs the clone.</param>
        /// <returns>The cloned object.</returns>
        public static T Clone<T>(this T obj, Func<T> cloneConstruction) where T: class
        {
            var cloner = new Cloner();
            return cloner.Clone(obj, cloneConstruction);
        }
    }
}