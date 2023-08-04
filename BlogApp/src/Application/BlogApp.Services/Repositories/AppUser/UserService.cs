using BlogApp.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Core.Utilities.Results;
using BlogApp.Core.Constants;
using Microsoft.AspNetCore.Http;
using IResult = BlogApp.Core.Utilities.Results.IResult;

namespace BlogApp.Services.Repositories.AppUser;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepository, UserManager<User> userManager,
        IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IDataResult<User?>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetAsync(id);
        return new SuccessDataResult<User?>(user, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<User>> GetByEmailAsync(string email)
    {
        var result = await _userRepository.GetWithPredicateAsync(x => x.Email.ToLower() == email.ToLower());

        if (result == null)
            return new ErrorDataResult<User>(Messages.UserNotFound, ApiStatusCodes.NotFound);

        return new SuccessDataResult<User>(result, ApiStatusCodes.Ok);
    }

    public async Task<IResult> AddAsync(User user, string password)
    {
        user.UserName = user.Email;
        user.SecurityStamp = Guid.NewGuid().ToString();
        user.PasswordHash = _passwordHasher.HashPassword(user, password);
        await _userRepository.CreateAsync(user);
        await AddUserClaimsAsync(user);
        return new SuccessResult(ApiStatusCodes.Created);
    }

    public async Task<IResult> UpdateAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
        return new SuccessResult(ApiStatusCodes.Ok);
    }

    public async Task<IResult> DeleteAsync(User user)
    {
        await _userRepository.DeleteAsync(user);
        return new SuccessResult(ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<User?>> GetCurrentUser()
    {
        var currentUser = _httpContextAccessor.HttpContext.User;
        if (currentUser == null)
            return new ErrorDataResult<User?>(Messages.SurvetNotFound, ApiStatusCodes.Unauthorized);

        string userEmail = currentUser?.FindFirstValue(ClaimTypes.Email);
        if (String.IsNullOrWhiteSpace(userEmail))
            return new ErrorDataResult<User?>(Messages.UserNotFound, ApiStatusCodes.NotFound);

        var result = await GetByEmailAsync(userEmail);
        if (result == null || !result.Success)
            return new ErrorDataResult<User?>(Messages.UserNotFound, ApiStatusCodes.NotFound);

        return new SuccessDataResult<User?>(result.Data, ApiStatusCodes.Ok);
    }

    private async Task AddUserClaimsAsync(User user)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.Email));
        claims.Add(new Claim(ClaimTypes.Role, "User"));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        await _userManager.AddClaimsAsync(user, claims);

    }
}


