using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Services.Repositories.Blog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IResult = BlogApp.Core.Utilities.Results.IResult;

namespace BlogApp.WebApi.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogsController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IDataResult<IEnumerable<BlogCardResponse?>>> GetBlogs(int page = 1)
    {
        return await _blogService.GetBlogsCardResponsesAsync(page);
    }

    [HttpGet("/blog/{url}")]
    public async Task<IDataResult<BlogCardResponse?>> BlogDetail(string url)
    {
        return await _blogService.GetBlogByUrlAsync(url);
    }

    [AllowAnonymous]
    [HttpGet("get-by-id/{id}")]
    public async Task<IDataResult<BlogCardResponse?>> GetBlog(int id)
    {
        return await _blogService.GetBlogAsync(id);
    }

    [HttpPut("update/{id}")]
    public async Task<IDataResult<bool>> UpdateBlog(UpdateBlogRequest blogRequest)
    {
        return await _blogService.UpdateAsync(blogRequest);
    }

    [HttpPost("create")]
    public async Task<IResult> Create(CreateNewBlogRequest createNewBlogRequest)
    {
        return await _blogService.CreateBlogAsync(createNewBlogRequest);
    }

    [HttpPost("upload-image")]
    public async Task<IDataResult<string>> Upload(IFormFile formFile)
    {
        return await _blogService.UploadBlogImage(formFile);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IResult> DeleteBlog(int id)
    {
        return await _blogService.DeleteAsync(id);
    }

    [HttpGet("get-user-blogs")]
    public async Task<IResult?> GetBlogsByUser()
    {
        return await _blogService.GetBlogsByUserAsync();
    }

    [HttpGet("get-saved-blogs")]
    public async Task<IResult> GetSavedBlogs()
    {
        return await _blogService.GetSavedBlogsAsync();
    }
    
    [AllowAnonymous]
    [HttpGet("blogs-count")]
    public async Task<IDataResult<BlogPaginationResponse>> GetBlogsCount()
    {
        return await _blogService.GetBlogsCount();
    }
}