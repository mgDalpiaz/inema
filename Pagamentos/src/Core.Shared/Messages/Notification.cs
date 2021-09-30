using Core.Shared.Base;
using Extension.Net;
using Newtonsoft.Json;
using System;

namespace Core.Shared.Messages
{
    /// <summary>
    /// Gera uma notificação para comunicar as camadas superiroes
    /// </summary>
    public class Notification : BaseEvent
    {
        #region [ Properties ]


        /// <summary>
        /// Chave unica gerada pela notificação
        /// </summary>
        [JsonIgnore]
        public Guid Id { get; set; }

        /// <summary>
        /// Tipo de notificação gerada com base no NotificationType
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Campo, Coluna, Entidade afetada pela Notificação 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Data de Ocorrência
        /// </summary>
        public DateTime OcorrencedAt { get; set; }

        /// <summary>
        /// Mensagem a ser retornada para a camada de Aprensentação
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Quando gerado uma mensagem de Exception ou Trace exibe o StackTrace
        /// </summary>
        public string TechnicalMessage { get; set; }

        /// <summary>
        /// Quando uma solicitação gera uma notificação de acesso não permitido
        /// </summary>
        [JsonIgnore]
        public bool Unauthorized { get; set; }

        /// <summary>
        /// Quando uma solicitação executa um agendamento ou processamento parcial
        /// </summary>
        [JsonIgnore]
        public bool Wainting { get; set; }

        #endregion

        #region [ Ctor ]

        public Notification()
        {
            this.Id = Guid.NewGuid();
            this.OcorrencedAt = DateTime.Now;
        }

        #endregion

        #region [ Factory ]

        /// <summary>
        /// Gera uma Notificação de Erro
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        /// <param name="techMsg"></param>
        /// <returns></returns>
        public new static Notification CreateError(string key, string msg, string techMsg = null) => SetNotification(key, msg, NotificationType.Error, techMsg);

        /// <summary>
        /// Gera uma notificação de Aviso
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        /// <param name="techMsg"></param>
        /// <returns></returns>
        public new static Notification CreateWarning(string key, string msg, string techMsg = null) => SetNotification(key, msg, NotificationType.Warning, techMsg);

        /// <summary>
        /// Gera uma notificação Informativa
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        /// <param name="techMsg"></param>
        /// <returns></returns>
        public new static Notification CreateInfo(string key, string msg, string techMsg = null) => SetNotification(key, msg, NotificationType.Info, techMsg);

        /// <summary>
        /// Gera uma notificação de sucesso
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        /// <param name="techMsg"></param>
        /// <returns></returns>
        public new static Notification CreateSuccess(string key, string msg, string techMsg = null) => SetNotification(key, msg, NotificationType.Success, techMsg);

        /// <summary>
        /// Gera uma notificação de erro interno de execução (Exceptions)
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public new static Notification CreateInternal(Exception ex) => SetNotification(ex, NotificationType.Internal);

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// Cria uma instância nova para a notificação do tipo Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal new static Notification SetNotification(Exception ex, NotificationType type)
        {
            #region [ CODE ]

            return new Notification
            {
                Key = ex.GetType().FullName,
                Message = ex.Message,
                TechnicalMessage = ex.ToTraceMessage(true),
                Type = type
            };

            #endregion
        }

        /// <summary>
        /// Cria uma nova instância para uma mensagem de notificação
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <param name="techMsg"></param>
        /// <returns></returns>
        internal new static Notification SetNotification(string key, string msg, NotificationType type, string techMsg = null)
        {
            #region [ CODE ]

            return new Notification
            {
                Key = key,
                Message = msg,
                TechnicalMessage = techMsg,
                Type = type,
                Unauthorized = type == NotificationType.Unauthorized
            };

            #endregion
        }



        #endregion
    }
}
