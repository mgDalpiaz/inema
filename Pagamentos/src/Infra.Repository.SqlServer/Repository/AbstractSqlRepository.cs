using Core.Shared;
using Core.Shared.Base;
using Infra.Repository.Shared.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository.SqlServer
{
    public abstract class AbstractSqlRepository<TEntity> : AbstractBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region [ Ctor ]

        public AbstractSqlRepository() : base()
        {

        }

        public AbstractSqlRepository(AbstractContext context) : base(context)
        {
        }


        #endregion

        public virtual TEntity Save(TEntity obj)
        {
            if (obj.IsAdd)
            {
                DbSet.Add(obj);                
            }   
            else
                DbSet.Update(obj);

            return obj;
        }

        public virtual TEntity Get(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
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
            DbSet.Remove(DbSet.Find(entity.Id));
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
