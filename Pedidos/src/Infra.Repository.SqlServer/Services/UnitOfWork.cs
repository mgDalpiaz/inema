using Core.Shared;
using Extension.Collections;
using System.Threading.Tasks;

namespace Infra.Repository.SqlServer
{
    /// <summary>
    /// Classe de gestao do Pacote de Trabalho do Banco Relacional, trata as Transacoes
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region [ Attr ]

        private readonly AbstractContext context;

        #endregion

        #region [ Ctor ]

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(AbstractContext context)
        {
            this.context = context;
        }


        #endregion

        #region [ Methods ]

        /// <summary>
        /// Comita uma transacao realizada com a base de dados
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Commit() => await context.SaveChangesAsync() > 0;

        /// <summary>
        /// Realiza o Rollback das Transacoes abertas
        /// </summary>
        /// <returns></returns>
        public Task Rollback() => Task.Run(() => this.context.ChangeTracker.Entries().ForEach(x => x.Reload()));

        #endregion



    }
}
