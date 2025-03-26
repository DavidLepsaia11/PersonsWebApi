using System.Linq.Expressions;

namespace PersonsWebApi.Infrastructure.Database.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity? Get(params object[] id);
        IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Set();

        void Insert(TEntity entity);
        void Update(TEntity entity);

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
