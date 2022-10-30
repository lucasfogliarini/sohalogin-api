using Microsoft.EntityFrameworkCore;
using SohaLogin.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace SohaLogin.Database
{
    internal class SohaLoginDatabase : ISohaLoginDatabase
    {
        readonly DbContext _dbContext;
        public SohaLoginDatabase(SohaLoginDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Query<TEntity>(bool asNoTracking = true) where TEntity : class, IEntity
        {
            var query = _dbContext.Set<TEntity>();
            if (asNoTracking)
            {
                return query.AsNoTracking();
            }
            return query;
        }
        public void Add<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Add(entity);
        }
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity
        {
            _dbContext.AddRange(entities);
        }

        public int Commit()
        {
            try
            {
                var changes = _dbContext.SaveChanges();
                return changes;
            }
            catch (DbUpdateException ex)
            {
                throw new ValidationException(ex.GetBaseException().Message);
            }
        }
        public async Task<int> CommitAsync()
        {
            try
            {
                var changes = _dbContext.SaveChangesAsync();
                return await changes;
            }
            catch (DbUpdateException ex)
            {
                throw new ValidationException(ex.GetBaseException().Message);
            }
        }
        public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Update(entity);
        }
        public void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Remove(entity);
        }
    }
}
