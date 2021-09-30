using System;
using MediatR;
using Newtonsoft.Json;

namespace Core.Shared.Base
{
    /// <summary>
    /// Classe de Extensão para Eventos de Dominio
    /// </summary>
    public abstract class BaseEvent : INotification
    {
        #region [ Properties ]

        [JsonIgnore]
        public Guid? SystemId { get; set; }

        public DateTime? Timestamp { get; private set; }

        #endregion

        #region [ Ctor ]

        public BaseEvent()
        {
            Timestamp = DateTime.Now;
        }

        #endregion

    }
}
