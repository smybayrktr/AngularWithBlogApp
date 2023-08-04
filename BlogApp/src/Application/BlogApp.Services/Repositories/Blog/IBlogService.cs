using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using Microsoft.AspNetCore.Http;
using IResult = BlogApp.Core.Utilities.Results.IResult;

namespace BlogApp.Services.Repositories.Blog;

public interface IBlogService
{
    Task<IDataResult<IEnumerable<BlogCardResponse?>>> GetBlogsCardResponsesAsync(int pageNumber = 1);
    Task<IDataResult<IEnumerable<BlogCardResponse?>>> GetBlogsByCategoryAsync(int categoryId);
    Task<IDataResult<BlogCardResponse?>> GetBlogAsync(int id);
    Task<IResult> CreateBlogAsync(CreateNewBlogRequest newBlogRequest);
    Task<IDataResult<IEnumerable<BlogCardResponse?>>> GetBlogsByUserAsync();
    Task<IDataResult<IEnumerable<BlogCardResponse?>>> GetSavedBlogsAsync();
    Task<IDataResult<BlogCardResponse?>> GetBlogByUrlAsync(string url);
    Task<IResult> DeleteAsync(int id);
    Task<IDataResult<bool>> UpdateAsync(UpdateBlogRequest updateBlogRequest);
    Task<IDataResult<UpdateBlogRequest?>> GetBlogAsUpdateBlogDtoAsync(int id);
    Task<IDataResult<string>> UploadBlogImage(IFormFile formFile);
    Task<IDataResult<BlogPaginationResponse>> GetBlogsCount();
}


