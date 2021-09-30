using System;

namespace CC.Extension.Primitives
{
    public static class DecimalExtension
    {
        /// <summary>
        /// Formata um Decimal para uma String sem pontuacao
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToCents(this decimal currency)
        {
            return String.Format("{0:0}", Math.Round(currency,2) * 100);
        }
    }
}
