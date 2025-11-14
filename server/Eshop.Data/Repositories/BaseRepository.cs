using Eshop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Eshop.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDBContext dbContext;

        protected readonly DbSet<TEntity> dbSet;

        public BaseRepository(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public TEntity? FindById(uint id)
        {
            return dbSet.Find(id);
        }

        public IList<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public TEntity Insert(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = dbSet.Add(entity);
            dbContext.SaveChanges();
            return entityEntry.Entity;
        }
    }
}
