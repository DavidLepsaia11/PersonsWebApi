using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonsWebApi.Infrastructure.Database.Interfaces;

namespace PersonsWebApi.Infrastructure.Database
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly PersonDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(PersonDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public virtual TEntity? Get(params object[] id) =>
            _dbSet.Find(id);

        public virtual void Insert(TEntity entity)
        {
            if (_context.Entry(entity) == null || _context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Add(entity);
            }
        }

        public virtual int SaveChanges() =>
             _context.SaveChanges();

        public virtual Task<int> SaveChangesAsync() =>
            _context.SaveChangesAsync();

        public virtual IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate) =>
            _context.Set<TEntity>().Where(predicate);

        public virtual IQueryable<TEntity> Set() =>
         _context.Set<TEntity>();

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
