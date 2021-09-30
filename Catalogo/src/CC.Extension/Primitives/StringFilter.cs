using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Extension.Primitives
{
    public static partial class StringExtension
    {
        /// <summary>
        /// Retorna apenas os números contidos em uma string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue">Valor padrão caso a string seja nula ou vazia</param>
        /// <returns></returns>
        public static string OnlyNumbers(this string value, string defaultValue = "") => value?.Where(Char.IsDigit).Aggregate(string.Empty, (a, c) => a = string.Concat(a, c)).IfNullOrWhiteSpace(defaultValue) ?? defaultValue;               
     
    }
}
