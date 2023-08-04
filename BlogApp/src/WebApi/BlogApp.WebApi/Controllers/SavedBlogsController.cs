using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Services.Repositories.SavedBlogAction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebApi.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/saved-blogs")]
[ApiController]
public class SavedBlogsController : ControllerBase
{
    private readonly ISavedBlogService _savedBlogService;

    public SavedBlogsController(ISavedBlogService savedBlogService)
    {
        _savedBlogService = savedBlogService;
    }

    [HttpPost("action")]
    public async Task<IDataResult<SaveBlogResponse>> SaveAction(int blogId)
    {
        return await _savedBlogService.SaveAction(blogId);
    }
}