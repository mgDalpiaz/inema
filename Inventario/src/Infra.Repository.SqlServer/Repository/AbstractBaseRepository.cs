using Infra.Repository.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository.SqlServer
{
    public abstract class AbstractBaseRepository<TEntity> : AbstractRepository, Shared.Interfaces.IRepository<TEntity> where TEntity : class
    {
        protected readonly AbstractContext Db;
        protected readonly DbSet<TEntity> DbSet;

        #region [ Ctor ]

        public AbstractBaseRepository() : base()
        {

        }

        public AbstractBaseRepository(AbstractContext context) : base()
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }


        #endregion

        public virtual TEntity Save(TEntity obj)
        {
            var isAdd = obj?.GetType()?.GetProperty("IsAdd")?.GetValue(obj).Equals(true) ?? false;

            if (isAdd)
            {
                DbSet.Add(obj);                
            }   
            else
                DbSet.Update(obj);

            return obj;
        }

        public virtual TEntity Get(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> Get()
        {
            return DbSet;
        }
        
        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(DbSet.Find(entity.GetType().GetProperty("id").GetValue(entity)));
        }

        public Task<int> CommitChanges()
        {
            return Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
