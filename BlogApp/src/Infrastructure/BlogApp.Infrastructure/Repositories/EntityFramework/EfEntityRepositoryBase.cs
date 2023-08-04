using BlogApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.Infrastructure.Repositories.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext

    {
        protected TContext _context;

        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TEntity entity)
        {

            /*
             * 2.Yol
             * var addedEntity = context.Entry(entity);
             * addedEntity.State = EntityState.Added;
             * await context.SaveChangesAsync();
             */
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();



        }

        public void Create(TEntity entity)
        {
            /*
             * 2.Yol
             * var addedEntity = context.Entry(entity);
             * addedEntity.State = EntityState.Added;
             * context.SaveChanges();
             */
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

        }

        public async Task DeleteAsync(TEntity entity)
        {

            /*
             * var deletedEntity = context.Entry(entity);
             * deletedEntity.State = EntityState.Deleted;
             * await context.SaveChangesAsync();
             */
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        public void Delete(TEntity entity)
        {
            /*
            * var deletedEntity = context.Entry(entity);
            * deletedEntity.State = EntityState.Deleted;
            * context.SaveChanges();
            */
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();

        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);

        }

        public TEntity? Get(int id)
        {

            return _context.Set<TEntity>().Find(id);

        }

        public async Task<IList<TEntity?>> GetAllAsync()
        {

            return await _context.Set<TEntity?>().AsNoTracking().ToListAsync();

        }

        public IList<TEntity?> GetAll()
        {

            return _context.Set<TEntity?>().AsNoTracking().ToList();

        }

        public async Task UpdateAsync(TEntity entity)
        {

            /* 2.Yol
             * var updatedEntity = context.Entry(entity);
             * updatedEntity.State = EntityState.Modified;
             * await context.SaveChangesAsync();
             */
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

        }

        public void Update(TEntity entity)
        {

            /*
             * 2.Yol
             * var updatedEntity = context.Entry(entity);
             * updatedEntity.State = EntityState.Modified;
             * context.SaveChanges();
             */
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();

        }

        public IList<TEntity> GetAllWithPredicate(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().AsNoTracking().Where(filter).ToList();

        }

        public async Task<IList<TEntity>> GetAllWithPredicateAsync(Expression<Func<TEntity, bool>> filter)
        {

            return await _context.Set<TEntity>().AsNoTracking().Where(filter).ToListAsync();

        }

        public TEntity? GetWithPredicate(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().FirstOrDefault(filter);
        }

        public async Task<TEntity?> GetWithPredicateAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }
    }
}

