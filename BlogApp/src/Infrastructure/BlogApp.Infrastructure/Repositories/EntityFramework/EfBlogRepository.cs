using BlogApp.Core.Constants;
using BlogApp.Entities;
using BlogApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Infrastructure.Repositories.EntityFramework
{
    public class EfBlogRepository : EfEntityRepositoryBase<Blog, BlogAppContext>, IBlogRepository
    {
        public EfBlogRepository(BlogAppContext blogAppContext) : base(blogAppContext)
        {

        }

        public async Task<IEnumerable<Blog?>> GetBlogsByCategoryAsync(int categoryId)
        {
            return await _context.Blogs.AsNoTracking()
                                        .Where(c => c.CategoryId == categoryId)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetBlogsByNameAsync(string title)
        {
            return await _context.Blogs.AsNoTracking().Where(c => c.Title.Contains(title)).ToListAsync();

        }

        public async Task<IEnumerable<BlogCardDto>> GetBlogCardDtos()
        {
            return _context.Blogs
            .Select(blog => new BlogCardDto
            {
                Id = blog.Id,
                CreatedAt = blog.CreatedAt,
                Title = blog.Title,
                Body = blog.Body,
                Image = blog.Image,
                Url = blog.Url,
                BookmarkImage = _context.SavedBlogs.Any(savedBlog => savedBlog.BlogId == blog.Id) ? Images.SavedBookmark : Images.UnsavedBookmark
            })
            .ToList();
        }

        public async Task<IEnumerable<BlogCardDto>> GetSavedBlogCardDtos(int userId)
        {
            return _context.SavedBlogs
            .Where(savedBlog => savedBlog.UserId == userId)
            .Join(
                _context.Blogs,
                savedBlog => savedBlog.BlogId,
                blog => blog.Id,
                (savedBlog, blog) => new BlogCardDto
                {
                    Id = blog.Id,
                    CreatedAt = blog.CreatedAt,
                    Title = blog.Title,
                    Body = blog.Body,
                    Image = blog.Image,
                    Url = blog.Url,
                    BookmarkImage = Images.SavedBookmark
                })
            .ToList();
        }

        public async Task<IEnumerable<BlogCardDto>> GetBlogDtosByCategory(int categoryId)
        {
            return _context.Categories
            .Where(category => category.Id == categoryId)
            .Join(
                _context.Blogs,
                category => category.Id,
                blog => blog.CategoryId,
                (savedBlog, blog) => new BlogCardDto
                {
                    Id = blog.Id,
                    CreatedAt = blog.CreatedAt,
                    Title = blog.Title,
                    Body = blog.Body,
                    Image = blog.Image,
                    Url = blog.Url,
                    BookmarkImage = _context.SavedBlogs.Any(savedBlog => savedBlog.BlogId == blog.Id) ? Images.SavedBookmark : Images.UnsavedBookmark
                })
            .ToList();
        }

    }
}

