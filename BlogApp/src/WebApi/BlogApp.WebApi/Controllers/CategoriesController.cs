using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Services.Repositories.Category;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet("get-all")]
    public async Task<IDataResult<IEnumerable<CategoryDisplayResponse?>>> GetBlogs()
    {
        return await _categoryService.GetCategoriesForListAsync();
    }
}


