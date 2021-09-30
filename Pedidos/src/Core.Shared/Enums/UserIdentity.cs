namespace Core.Shared
{
    public enum UserIdentity
    {
        /// <summary>
        /// Usuário de Sistema
        /// </summary>
        System,
        /// <summary>
        /// Usuário de Cliente
        /// </summary>
        Client,
        /// <summary>
        /// Usuário de Maquina - PDV, Totem, POS, SmartPhone
        /// </summary>
        Machine,
        /// <summary>
        /// Usuário de Integração
        /// </summary>
        Integration,
        /// <summary>
        /// Usuário de Administração da Geral dos Sistemas
        /// </summary>
        Administration
    }
}
