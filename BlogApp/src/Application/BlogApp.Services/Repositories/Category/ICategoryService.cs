using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Responses;

namespace BlogApp.Services.Repositories.Category;

public interface ICategoryService
{
    Task<IDataResult<IEnumerable<CategoryDisplayResponse?>>> GetCategoriesForListAsync();

}

