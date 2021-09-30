using System;

namespace Core.Shared
{
    /// <summary>
    /// Usado no metodo de Paginação para listar um objeto
    /// </summary>
    public interface IListCommand
    {
        #region [ Properties ]

        /// <summary>
        /// Tamanho da Página a ser devolvida
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Número da página
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Id da Unidade
        /// </summary>
        Guid? UnitId { get; set; }

        /// <summary>
        /// Trazer todos os registros Ativos e Inativos
        /// </summary>
        bool ListAll { get; set; }


        /// <summary>
        /// Paginação
        /// </summary>
        string Order { get; set; }

        /// <summary>
        /// Coluna que vai ser usada na ordenação
        /// </summary>
        public string ColumnOrder { get; set; }

        #endregion
    }
}
