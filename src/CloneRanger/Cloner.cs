using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace CloneRanger
{
    public class Cloner
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="objectToClone">The object to clone.</param>
        /// <param name="cloneConstruction">A function that constructs the clone.</param>
        /// <returns>The cloned object.</returns>
        public T Clone<T>(T objectToClone, Func<T> cloneConstruction) where T : class
        {
            if (objectToClone == null)
            {
                return default(T);
            }

            T clone = cloneConstruction.Invoke();

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
                foreach (PropertyInfo property in objectToClone.GetType().GetRuntimeProperties().Where(x => x.CanWrite))
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

        /// <summary>
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

            if (!objectToClone.GetType().GetTypeInfo().DeclaredConstructors.Any(x => !x.GetParameters().Any()))
            {
                throw new CloneRangerException($"The class {typeof(T).Name} has no parameterless constructor and no clone construction function has been provided.");                
            }

            return Clone(objectToClone, Activator.CreateInstance<T>);
        }

        private object CastGenericObjectToSpecificType(object genericObject, Type castToType)
        {
            MethodInfo method = GetType().GetTypeInfo().DeclaredMethods.First(x => x.Name == nameof(Clone) && x.GetParameters().Length == 1).MakeGenericMethod(castToType);
            return method.Invoke(this, new[] { genericObject });
        }

        private static bool IsCloneRequired(Type type)
        {
            return !(type.GetTypeInfo().IsPrimitive
                   || type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                   || type == typeof(string));
        }
    }
}
