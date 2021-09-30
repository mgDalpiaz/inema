namespace Extension.Collections
{
    /// <summary>
    /// Opcoes de Merge de Enumerable
    /// </summary>
    public enum MergeOption
    {
        /// <summary>
        /// Sobrescreve o valor atual com o valor do outro dicionário
        /// </summary>
        Override = 0,

        /// <summary>
        /// Matém o valor atual do dicionário
        /// </summary>
        Preserve = 1,

        /// <summary>
        /// Soma o valor caso seja Decimal ou Inteiro
        /// </summary>
        Sum = 2,

        /// <summary>
        /// Realiza a Diminuição caso seja Decimal ou Inteiro
        /// </summary>
        Subtract = 3,

        /// <summary>
        /// Mantem o maior valor
        /// </summary>
        Bigger = 4,

        /// <summary>
        /// Mantem o menor valor
        /// </summary>
        Minor = 5


    }
}
