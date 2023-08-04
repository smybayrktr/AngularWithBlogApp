using BlogApp.Entities;
using BlogApp.Infrastructure.Data;

namespace BlogApp.Infrastructure.Repositories.EntityFramework
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category, BlogAppContext>, ICategoryRepository
    {
        public EfCategoryRepository(BlogAppContext blogAppContext) : base(blogAppContext)
        {

        }
    }
}

