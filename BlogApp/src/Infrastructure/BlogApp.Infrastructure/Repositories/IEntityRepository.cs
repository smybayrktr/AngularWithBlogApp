using System.Linq.Expressions;
using BlogApp.Entities;

namespace BlogApp.Infrastructure.Repositories
{

    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T? Get(int id);
        Task<T?> GetAsync(int id);

        IList<T?> GetAll();
        Task<IList<T?>> GetAllAsync();

        IList<T> GetAllWithPredicate(Expression<Func<T, bool>> filter);
        Task<IList<T>> GetAllWithPredicateAsync(Expression<Func<T, bool>> filter);

        T? GetWithPredicate(Expression<Func<T, bool>> filter);
        Task<T?> GetWithPredicateAsync(Expression<Func<T, bool>> filter);

        void Create(T entity);
        Task CreateAsync(T entity);

        void Delete(T entity);
        Task DeleteAsync(T entity);

        void Update(T entity);
        Task UpdateAsync(T entity);

    }
}

