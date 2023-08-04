using AutoMapper;
using BlogApp.Core.Constants;
using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Services.Repositories.AppUser;
using BlogApp.Entities;

namespace BlogApp.Services.Repositories.SavedBlogAction;

public class SavedBlogService : ISavedBlogService
{
    private readonly ISavedBlogRepository _savedBlogRepository;
    private readonly IUserService _userService;

    public SavedBlogService(ISavedBlogRepository savedBlogRepository, IUserService userService)
    {
        _savedBlogRepository = savedBlogRepository;
        _userService = userService;
    }

    public async Task<IDataResult<SaveBlogResponse>> SaveAction(int blogId)
    {
        var currentUser = await _userService.GetCurrentUser();
        var saveBlogResponse = new SaveBlogResponse();
        if (!currentUser.Success)
        {
            saveBlogResponse.Message = Messages.Unauthorized;
            saveBlogResponse.BookmarkImage = Images.UnsavedBookmark;
            return new ErrorDataResult<SaveBlogResponse>(saveBlogResponse, ApiStatusCodes.Unauthorized);
        }
        var checkSavedBlog = await _savedBlogRepository.GetWithPredicateAsync(x => x.BlogId == blogId);

        if (checkSavedBlog == null)
        {
            var savedBlog = new SavedBlog
            {
                UserId = currentUser.Data.Id,
                BlogId = blogId
            };
            await _savedBlogRepository.CreateAsync(savedBlog);
            saveBlogResponse.BookmarkImage = Images.SavedBookmark;
            saveBlogResponse.Message = Messages.SavedBookmark;
        }
        else
        {
            await _savedBlogRepository.DeleteAsync(checkSavedBlog);
            saveBlogResponse.BookmarkImage = Images.UnsavedBookmark;
            saveBlogResponse.Message = Messages.UnsavedBookmark;
        }
        return new SuccessDataResult<SaveBlogResponse>(saveBlogResponse, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<SavedBlog?>> GetSavedBlogByBlogIdAsync(int blogId)
    {
        var user = await _userService.GetCurrentUser();
        if (!user.Success) return new ErrorDataResult<SavedBlog?>(ApiStatusCodes.Unauthorized);

        var result = await _savedBlogRepository.GetWithPredicateAsync(x => x.BlogId == blogId);
        return new SuccessDataResult<SavedBlog?>(result, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<IEnumerable<SavedBlog?>>> GetSavedBlogsByUserAsync()
    {
        var user = await _userService.GetCurrentUser();
        if (!user.Success) return new ErrorDataResult<IEnumerable<SavedBlog?>>(ApiStatusCodes.Unauthorized);

        var result = await _savedBlogRepository.GetAllWithPredicateAsync(u => u.UserId == user.Data.Id);
        return new SuccessDataResult<IEnumerable<SavedBlog?>>(result, ApiStatusCodes.Ok);
    }
}

