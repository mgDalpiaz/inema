using System;
using System.Collections.Generic;
using System.Linq;

namespace Extension.Collections
{
    /// <summary>
    /// Extende IEnumerable
    /// </summary>
    public static partial class EnumerableExtension
    {
              
       
        /// <summary>
		/// Permite executar uma action na iteração de um IEnumerable, assim como é possivel com tipos IList.ForEach
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="action"></param>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            #region [ Code ]
            if (source == null)
                return;

            foreach (T element in source)
            {
                action(element);
            }
            #endregion
        }
      
    }
}
