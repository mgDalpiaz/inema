using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Messages;
using MediatR;

namespace App.Shared
{
    public class CrossMessageHandler : INotificationHandler<CrossMessage>
    {
        #region [ Properties ]

        public Dictionary<string, dynamic> Messages { get; set; }

        #endregion

        #region [ Ctor ]

        public CrossMessageHandler()
        {
            this.Messages = new Dictionary<string, dynamic>();
        }

        #endregion

        #region [ Methods ]

        public Task Handle(CrossMessage message, CancellationToken cancellationToken)
        {
            if (message == null)
                return Task.CompletedTask;

            if (Messages.ContainsKey(message.TypeName))
                Messages[message.TypeName] = Convert.ChangeType(message.Message, message.Type);
            else
                Messages.TryAdd(message.TypeName, Convert.ChangeType(message.Message, message.Type));

            return Task.CompletedTask;
        }

        public T GetMessage<T>() where T : class
        {
            dynamic value = null;

            this.Messages.TryGetValue(typeof(T).Name, out value);

            if (value == null)
                return null;

            return Convert.ChangeType(value, typeof(T));
        }

        public object GetMessage(string type)
        {
            dynamic value = null;

            this.Messages.TryGetValue(type, out value);

            if (value == null)
                return null;

            return value;
        }

        #endregion

        #region [ Garbage ]

        public void Dispose()
        {
            Messages = new Dictionary<string, dynamic>();
        }

        #endregion
    }
}
