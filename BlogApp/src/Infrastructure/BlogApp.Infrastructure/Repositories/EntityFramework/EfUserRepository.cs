using BlogApp.Entities;
using BlogApp.Infrastructure.Data;

namespace BlogApp.Infrastructure.Repositories.EntityFramework
{
	public class EfUserRepository : EfEntityRepositoryBase<User, BlogAppContext>, IUserRepository
    {
        public EfUserRepository(BlogAppContext blogAppContext) : base(blogAppContext)
        {

        }
    }
}

