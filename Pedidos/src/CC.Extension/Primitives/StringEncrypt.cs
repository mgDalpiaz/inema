using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Extension.Primitives
{
    public static partial class StringExtension
    {

        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "HR$2pIjHR$2pIj12";

        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;

        /// <summary>
        /// Criptogragrafa a String em Base64
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBase64Encode(this string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

        /// <summary>
        /// Retira o Base64 de uma String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBase64Decode(this string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));

        /// <summary>
        /// Calcula o MD5 da String
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static string EncryptToMd5(this string @string)
        {
            #region [ CODE ]

            var bytes = Encoding.UTF8.GetBytes(@string);

            using (var hasher = MD5.Create())
            {
                var hash = hasher.ComputeHash(bytes);

                return hash.Aggregate(string.Empty, (a, x) => a += x.ToString("x2"));
            }

            #endregion
        }

    }
}
