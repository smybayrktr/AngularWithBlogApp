using BlogApp.Entities;

namespace BlogApp.Infrastructure.Repositories
{
    public interface IBlogRepository : IEntityRepository<Blog>
    {
        Task<IEnumerable<Blog?>> GetBlogsByCategoryAsync(int categoryId);

        Task<IEnumerable<Blog>> GetBlogsByNameAsync(string title);

        Task<IEnumerable<BlogCardDto>> GetBlogCardDtos();

        Task<IEnumerable<BlogCardDto>> GetSavedBlogCardDtos(int userId);

        Task<IEnumerable<BlogCardDto>> GetBlogDtosByCategory(int categoryId);
    }
}

