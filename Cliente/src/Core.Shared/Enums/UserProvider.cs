namespace Core.Shared
{
    public enum UserProvider
    {
        /// <summary>
        /// Usuário Interno do Sistema
        /// </summary>
        Internal,
        /// <summary>
        /// Usuário de Redes Sóciais
        /// </summary>
        Social,
        /// <summary>
        /// Usuário de Integração AD, OpenLDAP, OpenID
        /// </summary>
        Integration
    }
}
