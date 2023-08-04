using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Entities;

namespace BlogApp.Services.Repositories.SavedBlogAction;

public interface ISavedBlogService
{
    Task<IDataResult<SaveBlogResponse>> SaveAction(int blogId);
    Task<IDataResult<SavedBlog?>> GetSavedBlogByBlogIdAsync(int blogId);
    Task<IDataResult<IEnumerable<SavedBlog?>>> GetSavedBlogsByUserAsync();
}

