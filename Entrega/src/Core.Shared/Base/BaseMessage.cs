using Core.Shared.Messages;
using System.Collections.Generic;

namespace Core.Shared.Base
{
    /// <summary>
    /// Padrão de Retorno para qualquer mensagem que for ser aprensentada (exposta)
    /// </summary>
    public abstract class BaseMessage
    {
     
        #region [ Properties ]

        /// <summary>
        /// Se a mensagem é valida e não apresentou problemas de processamento
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Lista de notificações a serem aprensetados a camada de aprensentação
        /// </summary>
        public List<Notification> Notifications { get; set; } = new List<Notification>();

        #endregion

        #region [ Ctor ]

        /// <summary>
        /// Ctor
        /// </summary>
        public BaseMessage()
        {
            this.IsValid = true;
        }

        #endregion

    }
}
