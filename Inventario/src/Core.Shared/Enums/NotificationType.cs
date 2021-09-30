namespace Core.Shared
{
    public enum NotificationType
    {
        /// <summary>
        /// Seta uma mensagem como erro interno do Servidor
        /// </summary>
        Internal,
        /// <summary>
        /// Seta como uma mensagem de Erro de Negócio 
        /// </summary>
        Error,
        /// <summary>
        /// Seta uma mensagem como Warning
        /// </summary>
        Warning,
        /// <summary>
        /// Seta uma mensagem com Informatica
        /// </summary>
        Info,
        /// <summary>
        /// Seta uma mensagem como sucesso
        /// </summary>
        Success,
        /// <summary>
        /// Seta Mensagem como n autorizada a acessar o recurso
        /// </summary>
        Unauthorized
    }
}
