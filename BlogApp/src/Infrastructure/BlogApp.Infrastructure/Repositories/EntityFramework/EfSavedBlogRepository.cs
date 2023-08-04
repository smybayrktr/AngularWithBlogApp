using BlogApp.Entities;
using BlogApp.Infrastructure.Data;

namespace BlogApp.Infrastructure.Repositories.EntityFramework
{
    public class EfSavedBlogRepository : EfEntityRepositoryBase<SavedBlog, BlogAppContext>, ISavedBlogRepository
    {
        public EfSavedBlogRepository(BlogAppContext context) : base(context)
        {
        }
    }
}

