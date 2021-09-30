using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository.Shared.Interfaces
{
    /// <summary>
    /// Repositório padrão sem identificação de classe
    /// </summary>
    public interface IRepository
    {

    }

    /// <summary>
    /// Repositorio com Tipagem de Dados
    /// </summary>
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        TEntity Save(TEntity entity);

        void Remove(Guid id);

        void Remove(TEntity entity);

        IQueryable<TEntity> Get();

        TEntity Get(Guid id);

        Task<int> CommitChanges();

    }

}
