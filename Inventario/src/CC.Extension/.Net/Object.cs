using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Extension.Net
{
    /// <summary>
    /// Classe para extender funcoes do Object e facilitar manipulacao de objetos
    /// </summary>
    public static class ObjectExtension
    {
        
        public static bool IsCollectionType(PropertyInfo pi)
        {
            return
                 pi.PropertyType.IsSubclassOf(typeof(IEnumerable))
                 || pi.PropertyType.IsSubclassOf(typeof(IList))
                 || pi.PropertyType.IsSubclassOf(typeof(ICollection))
                 || pi.PropertyType.Name.Contains("List`1");

        }
    }
}
