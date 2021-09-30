using System;

namespace Extension.Collections
{
    public static partial class ArrayExtension
    {

        /// <summary>
        /// Implementa ForEach para uso com Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            #region [ Code ]
            if (array == null)
                return;

            foreach (T element in array)
            {
                action(element);
            }
            #endregion
        }

    }
}
