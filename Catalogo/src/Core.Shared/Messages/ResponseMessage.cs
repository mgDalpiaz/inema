using Core.Shared.Base;

namespace Core.Shared.Messages
{
    public class ResponseMessage<T> : BaseMessage where T : class
    {
        #region [ Properties ]

        /// <summary>
        /// Dados retornados na Mensagem
        /// </summary>
        public T Data { get; set; }

        #endregion

        #region [ Ctor ]

        public ResponseMessage(T result) 
            : base()
        {
            this.Data = result;
        }

        public ResponseMessage()
            : base()
        {
        }

        #endregion

        #region [ Factory ]

        public static ResponseMessage<T> Invalid()
        {
            return new ResponseMessage<T>()
            {
                IsValid = false
            };
        }

        public static ResponseMessage<T> Valid(T data)
        {
            return new ResponseMessage<T>()
            {
                IsValid = true,
                Data = data
            };
        }

        public static ResponseMessage<T> Valid()
        {
            return new ResponseMessage<T>()
            {
                IsValid = true
            };
        }

        #endregion

    }
}
