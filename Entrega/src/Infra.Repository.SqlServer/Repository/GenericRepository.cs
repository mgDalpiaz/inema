using System;

namespace Infra.Repository.SqlServer.Repository
{
    public class GenericRepository<TEntity> : AbstractBaseRepository<TEntity> where TEntity : class
    {
    
        #region [ Ctor ]

        public GenericRepository() : base()
        {
        }

        public GenericRepository(AbstractContext context) : base(context)
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
