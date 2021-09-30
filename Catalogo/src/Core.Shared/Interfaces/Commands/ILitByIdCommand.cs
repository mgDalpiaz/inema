using System;

namespace Core.Shared
{
    /// <summary>
    /// Usado no metodo de Paginação para listar um objeto
    /// </summary>
    public interface IListByIdCommand
    {
        #region [ Properties ]

        /// <summary>
        /// Id a ser recuperado da Base de Dados
        /// </summary>
        Guid Id { get; set; }

        #endregion
    }
}
