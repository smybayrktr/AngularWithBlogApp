using AutoMapper;
using BlogApp.Core.Constants;
using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Responses;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Services.Extensions;

namespace BlogApp.Services.Repositories.Category;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }
    public async Task<IDataResult<IEnumerable<CategoryDisplayResponse?>>> GetCategoriesForListAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var responses = categories.ConvertToDto(_mapper);
        return new SuccessDataResult<IEnumerable<CategoryDisplayResponse>>(responses, ApiStatusCodes.Ok);
    }
}


