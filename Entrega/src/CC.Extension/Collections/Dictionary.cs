using System;
using System.Collections.Generic;

namespace Extension.Collections
{
    /// <summary>
    /// Extende IDictionary
    /// </summary>
    public static partial class DictionaryExtension
    {

        /// <summary>
		/// Copia o conteúdo do dicionário <paramref name="self"/>, mas mantém os valores originais
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="source"></param>
		public static void MergeWith<TKey, TValue>(this IDictionary<TKey, TValue> self, IDictionary<TKey, TValue> source) => self.MergeWith(source, MergeOption.Preserve);

        /// <summary>
        /// Copia o conteúdo do dicionário <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="source"></param>
        /// <param name="option">Em caso de já existir a chave na dicionário, define o que deve ser feito, se será sobrescrito ou o valor será preservado</param>
        public static IDictionary<TKey, TValue> MergeWith<TKey, TValue>(this IDictionary<TKey, TValue> self, IDictionary<TKey, TValue> source, MergeOption option)
        {
            #region [ Code ]
                        switch (option)
            {
                case MergeOption.Override:
                    foreach (var pair in source)
                        self[pair.Key] = pair.Value;
                    break;
                default:
                case MergeOption.Preserve:
                    foreach (var pair in source)
                        if (!self.ContainsKey(pair.Key))
                            self[pair.Key] = pair.Value;
                    break;
                case MergeOption.Bigger:
                    foreach (var pair in source)
                        if(pair.Value is Decimal || pair.Value is int)
                        {
                            var sValue = Convert.ToDecimal(pair.Value);
                            var tValue = Convert.ToDecimal(self[pair.Key]);
                            self[pair.Key] = sValue > tValue ? pair.Value : self[pair.Key];
                        }
                    break;
                case MergeOption.Minor:
                    foreach (var pair in source)
                        if (pair.Value is Decimal || pair.Value is int)
                        {
                            var sValue = Convert.ToDecimal(pair.Value);
                            var tValue = Convert.ToDecimal(self[pair.Key]);
                            self[pair.Key] = sValue < tValue ? pair.Value : self[pair.Key];
                        }
                    break;                
            }

            return self;
            #endregion
        }

        /// <summary>
        /// Adiciona ou atualiza um valor de um dicionário
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value)
        {
            #region [ Code ]
            if (self.ContainsKey(key))
                self[key] = value;
            else
                self.Add(key, value);
            #endregion
        }             
       
    }
}
