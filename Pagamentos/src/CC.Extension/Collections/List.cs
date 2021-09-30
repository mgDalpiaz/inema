using System;
using System.Collections.Generic;

namespace Extension.Collections
{
    /// <summary>
    /// Extende IList
    /// </summary>
    public static partial class ListExtension
    {
        #region [ Add Extensions ]

        /// <summary>
        /// Adiciona o conteúdo da coleção <paramref name="collection"/> na lista
        /// </summary>
        /// <typeparam name="TItem">Tipo do item da lista</typeparam>
        /// <param name="list">Lista</param>
        /// <param name="collection">Coleção de itens que serão adicionados na listagem <paramref name="list"/>.</param>
        public static void Add<TItem>(this IList<TItem> list, IEnumerable<TItem> collection)
        {
            #region [ Code ]
            if (list == null) throw new ArgumentNullException("list");

            if (collection != null)
                foreach (var item in collection)
                    list.Add(item);
            #endregion
        }

        /// <summary>
        /// Adiciona cada instância gerada pela função <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="list"></param>
        /// <param name="source">Função que realiza a criação dos itens que serão adicionados na lista</param>
        /// <remarks>
        /// Adiciona os objetos gerados pela função <paramref name="source"/> até 
        /// retorna null
        /// </remarks>
        public static void Add<TItem>(this IList<TItem> list, Func<TItem> source)
            where TItem : class
        {
            #region [ Code ]
            if (list == null) throw new ArgumentNullException("list");

            TItem item;

            while ((item = source()) != null)
                list.Add(item);
            #endregion
        }

        #endregion

        #region [ Search Extensions ]

        /// <summary>
        /// Realiza uma pesquisa binária na lista procurando o Index da Comparacao passada
        /// </summary>
        /// <remarks>
        /// .IndexSearch(ws => UrlComparer.Compare(lookingFor, ws.UrlPart));
        /// </remarks>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="list">Lista ordenada pela propriedade que será pesquisada</param>
        /// <param name="comparer">Função que será chamada para verificar se um item existe na lista</param>
        /// <returns>Retorna o índice do item que foi encontrado na lista ou -1 se não encontrou no array</returns>
        public static int IndexSearch<TItem>(this IList<TItem> list, Func<TItem, int> comparer)
        {
            #region [ Code ]
            if (list == null) throw new ArgumentNullException("list");
            if (comparer == null) throw new ArgumentNullException("comparer");

            int start = 0;
            int end = list.Count - 1;
            int index, compResult;

            while (start <= end)
            {
                index = (start + end) / 2;
                compResult = comparer(list[index]);

                if (compResult > 0)
                    start = index + 1;
                else if (compResult < 0)
                    end = index - 1;
                else
                    return index;
            }

            return -1;
            #endregion
        }

        #endregion      
    }
}
