using System;
using System.Collections;
using System.Reflection;

namespace CloneRanger
{
    public class Cloner
    {
        /// <summary>
        /// Clone an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="objectToClone">The object to clone.</param>
        /// <returns>The cloned object.</returns>
        public T Clone<T>(T objectToClone) where T : class
        {
            if (objectToClone == null)
            {
                return default(T);
            }

            var clone = Activator.CreateInstance<T>();

            if (objectToClone is IList)
            {
                var objectToCloneList = (IList)objectToClone;

                foreach (object objectToCloneListItem in objectToCloneList)
                {
                    if (IsCloneRequired(objectToCloneListItem.GetType()))
                    {                        
                        ((IList)clone).Add(CastGenericObjectToSpecificType(objectToCloneListItem, objectToCloneList.GetType().GetTypeInfo().GenericTypeArguments[0]));                    
                    }
                    else
                    {
                        ((IList)clone).Add(objectToCloneListItem);
                    }
                }
            }
            else
            {
                foreach (PropertyInfo property in objectToClone.GetType().GetRuntimeProperties())
                {
                    if (IsCloneRequired(property.PropertyType))
                    {                        
                        property.SetValue(clone, CastGenericObjectToSpecificType(property.GetValue(objectToClone), property.PropertyType.IsConstructedGenericType ? property.PropertyType.GenericTypeArguments[0] : property.PropertyType));                        
                    }
                    else
                    {
                        property.SetValue(clone, property.GetValue(objectToClone));
                    }
                }
            }

            return clone;
        }

        private object CastGenericObjectToSpecificType(object obj, Type castToType)
        {
            MethodInfo method = GetType().GetTypeInfo().GetDeclaredMethod(nameof(Clone)).MakeGenericMethod(castToType);
            return method.Invoke(this, new[] { obj });
        }

        private static bool IsCloneRequired(Type type)
        {
            return !(type.GetTypeInfo().IsPrimitive
                   || type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                   || type == typeof(string));
        }
    }
}
