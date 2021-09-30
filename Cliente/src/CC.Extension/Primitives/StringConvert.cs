using System;
using System.IO;

namespace Extension.Primitives
{
    public static partial class StringExtension
    {
        /// <summary>
        /// Convert uma String para Long se possuir numeros
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long? ToInt64(this string value)
        {
            #region [ Code ]
            if (long.TryParse(value.OnlyNumbers(), out long number))
                return number;

            return null;
            #endregion
        }

        /// <summary>
        /// Convert uma String para Decimal se possuir numeros
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this string value)
        {
            #region [ Code ]
            if (decimal.TryParse(value.OnlyNumbers(), out decimal number))
                return number;

            return null;
            #endregion
        }

        /// <summary>
        /// Convert uma String para Int se possuir numeros
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? ToInt(this string value)
        {
            #region [ Code ]
            if (int.TryParse(value.OnlyNumbers(), out int number))
                return number;

            return null;
            #endregion
        }

        /// <summary>
        /// Convert uma string para o tipo do enum.
        /// Se não encontrar o valor retorna o valor default do enum.
        /// </summary>
        public static TEnum ToEnum<TEnum>(this System.String value) where TEnum : struct
        {
            #region [ Code ]
            Enum.TryParse<TEnum>(value, true, out TEnum parsedEnum);
            return parsedEnum;
            #endregion
        }
               

    }
}
