using System.ComponentModel;

namespace CC.Common
{
    public enum AppEnvironment
    {
        /// <summary>
        /// Ambiente Produção.
        /// </summary>
        [Description("Produção")]
        Production,
        /// <summary>
        /// Ambiente Homologação.
        /// </summary>
        [Description("Homologação")]
        Staging,
        /// <summary>
        /// Ambiente Testes.
        /// </summary>
        [Description("Teste")]
        Testing,
        /// <summary>
        /// Ambiente de Desenvolvimento.
        /// </summary>
        [Description("Desenvolvimento")]
        Development
    }

}
