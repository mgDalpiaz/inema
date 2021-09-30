using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared;
using Core.Shared.Messages;
using MediatR;

namespace App.Shared
{
    public class NotificationHandler :
        INotificationHandler<Notification>
    {
        #region [ Attr ]

        private CrossMessageHandler _messages;
       
        #endregion

        #region [ Properties ]

        /// <summary>
        /// Lista de Notificações a serem aprensentadas
        /// </summary>
        public List<Notification> Notifications { get; private set; }

        #endregion

        #region [Calculate Properties]

        /// <summary>
        /// Possui notificações
        /// </summary>
        public bool HasNotification => Notifications.Any();

        /// <summary>
        /// Erro de negócio ou conhecido
        /// </summary>
        public bool IsError => Notifications.Any(x => x.Type == NotificationType.Error);

        /// <summary>
        /// Erro Interno durante o processamento
        /// </summary>
        public bool IsInternalError => Notifications.Any(x => x.Type == NotificationType.Internal);

        /// <summary>
        /// Não possui nenhum erro na execução
        /// </summary>
        public bool IsValid => !(IsError || IsInternalError);

        /// <summary>
        /// Requisitado porém aguardando Processar em background
        /// </summary>
        public bool Wainting => Notifications.Any(x => x.Wainting);

        /// <summary>
        /// Algum processamento retorna uma execução sem permissão.
        /// </summary>
        public bool Unauthorized => Notifications.Any(x => x.Unauthorized);

        #endregion

        #region [ Ctor ]

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="messages"></param>
        public NotificationHandler(INotificationHandler<CrossMessage> messages)
        {
            Notifications = new List<Notification> { };
            _messages = (CrossMessageHandler)messages;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Armazena a notificação em uma lista para ser exibida na camada superior
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(Notification message, CancellationToken cancellationToken)
        {
           Notifications.Add(message);
           return Task.CompletedTask;
        }

        #endregion

        #region [ Garbage ]

        public void Dispose()
        {
            Notifications = new List<Notification> { };
        }

        #endregion

    }
}
