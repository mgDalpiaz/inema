using Core.Shared.Base;
using System;

namespace Core.Shared.Messages
{
    /// <summary>
    /// Classe usada para passar um objeto entre as camadas superiores e inferiores sem precisar usar Referência ou injeção direta.
    /// Define o contexto da Aplicação como Dados do usuário autenticado e do cliente
    /// </summary>
    public class CrossMessage : BaseEvent
    {

        #region [ Properties ]

        /// <summary>
        /// Tipo do Objeto Passado typrof().Name geralmente mais em casos de Interface pode ser passado o nome da classe concreta.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Instância do objeto a ser passado.
        /// </summary>
        public dynamic Message { get; set; }

        /// <summary>
        /// Tipo do Objeto armazenado em Message
        /// </summary>
        public Type Type { get; set; }

        #endregion

        #region [ Ctor ]

        /// <summary>
        /// Ctor
        /// </summary>
        public CrossMessage() : base()
        {

        }

        #endregion

        #region [ Factory ]

        /// <summary>
        /// Cria uma mensagem com base no objeto e seu tipo
        /// </summary>
        /// <typeparam name="T">Typeof / Classe concreta ou Interface</typeparam>
        /// <param name="value">Instância</param>
        /// <returns>CrossMessage</returns>
        public static CrossMessage CreateCrossMessage<T>(T value) where T : class
        {
            if (value == null)
                return null;

            return new CrossMessage { Type = value.GetType(), Message = value, TypeName = value.GetType().Name };
        }

        /// <summary>
        /// Cria uma mensagem com base no objeto e seu tipo e apresenta o mesmo com o nome customizado
        /// </summary>
        /// <typeparam name="T">Typeof / Classe concreta ou Interface</typeparam>
        /// <param name="value">Instância</param>
        /// <param name="typename">nome a ser usado para resgatar o objeto</param>
        /// <returns>CrossMessage</returns>
        public static CrossMessage CreateCrossMessage<T>(T value, string typename) where T : class
        {
            if (value == null)
                return null;

            return new CrossMessage { Type = value.GetType(), Message = value, TypeName = typename };
        }

        #endregion

    }
}
