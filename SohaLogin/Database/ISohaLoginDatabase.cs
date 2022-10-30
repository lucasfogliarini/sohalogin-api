using SohaLogin.Database.Entities;

namespace SohaLogin.Database
{
    public interface ISohaLoginDatabase
    {
        IQueryable<TEntity> Query<TEntity>(bool asNoTracking = true) where TEntity : class, IEntity;
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task<int> CommitAsync();
        int Commit();
    }
}
