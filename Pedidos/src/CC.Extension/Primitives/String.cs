using Extension.Collections;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Extension.Primitives
{
    public static partial class StringExtension
    {
      
        /// <summary>
        /// Torna a primeira letra Maiuscula
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UpperCamelCase(this string str) => !string.IsNullOrEmpty(str) && str.Length > 1 ? Char.ToUpperInvariant(str[0]) + str.Substring(1) : str;

        /// <summary>
		/// Retorna a string de destino caso a string de entrada seja vazia, null ou white space
		/// </summary>
		/// <param name="input"></param>
		/// <param name="destination"></param>
		/// <returns></returns>
		public static string IfNullOrWhiteSpace(this string input, string destination) => string.IsNullOrWhiteSpace(input) ? destination ?? string.Empty : input;
             
       
    }
}
