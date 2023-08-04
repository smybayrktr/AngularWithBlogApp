using BlogApp.Core.Utilities.Results;
using BlogApp.Entities;

namespace BlogApp.Services.Repositories.AppUser;

public interface IUserService
{
    Task<IDataResult<User?>> GetByIdAsync(int id);
    Task<IResult> AddAsync(User user, string password);
    Task<IResult> UpdateAsync(User user);
    Task<IResult> DeleteAsync(User user);
    Task<IDataResult<User?>> GetByEmailAsync(string email);
    Task<IDataResult<User?>> GetCurrentUser();
}

