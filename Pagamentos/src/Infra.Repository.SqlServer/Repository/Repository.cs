using Core.Shared;
using Core.Shared.Base;
using System;

namespace Infra.Repository.SqlServer.Repository
{
    public class Repository<TEntity> : AbstractSqlRepository<TEntity> where TEntity : BaseEntity
    {
    
        #region [ Ctor ]

        public Repository() : base()
        {

        }

        public Repository(AbstractContext context) : base(context)
        {
        }

        #endregion
      
        public void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
