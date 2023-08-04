using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BlogApp.Core.Constants;
using BlogApp.Core.Utilities.Jwt;
using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.Entities;
using BlogApp.Services.Extensions;
using BlogApp.Services.Repositories.AppUser;
using BlogApp.Services.Repositories.Schedule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.Services.Repositories.Auth;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly TokenOption _tokenOption;

    public AuthService(IPasswordHasher<User> passwordHasher,
        IUserService userService, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _passwordHasher = passwordHasher;
        _userService = userService;
        _userManager = userManager;
        _mapper = mapper;
        _tokenOption = configuration.GetSection("TokenOption").Get<TokenOption>();
    }

    public async Task<IDataResult<AccessToken>> Register(UserRegisterRequest userRegisterRequest)
    {
        var checkUserByEmail = await checkUserExistsByEmail(userRegisterRequest.Email);
        if (checkUserByEmail) return new ErrorDataResult<AccessToken>(Messages.UserNotFound, ApiStatusCodes.NotFound);

        var user = userRegisterRequest.ConvertToDto(_mapper);
        await _userService.AddAsync(user, userRegisterRequest.Password);

        var userClaims = GetClaims(user);
        var accessToken = CreateAccessToken(userClaims);
        ScheduleService.ScheduleSendRegisterEmail(userRegisterRequest.Email, userRegisterRequest.Name);
        return new SuccessDataResult<AccessToken>(accessToken, ApiStatusCodes.Ok);
    }

    public async Task<IDataResult<AccessToken>> Login(UserLoginRequest userLoginRequest)
    {
        var userToFind = await _userService.GetByEmailAsync(userLoginRequest.Email);
        if (!userToFind.Success)
            return new ErrorDataResult<AccessToken>(Messages.EmailAlreadyRegistered, ApiStatusCodes.NotFound);

        var checkPassword = verifyUserPassword(userToFind.Data, userToFind.Data.PasswordHash, userLoginRequest.Password);
        if (!checkPassword)
            return new ErrorDataResult<AccessToken>(Messages.WrongEmailOrPassword, ApiStatusCodes.BadRequest);


        var userClaims = GetClaims(userToFind.Data);
        var accessToken = CreateAccessToken(userClaims);

        return new SuccessDataResult<AccessToken>(accessToken, ApiStatusCodes.Ok);
    }

    public async Task<IResult> Logout()
    {

        return new SuccessResult(ApiStatusCodes.Ok);
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, "User"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        return claims;
    }

    private AccessToken CreateAccessToken(IEnumerable<Claim> claims)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpirationInMinutes);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenOption.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: claims,
            signingCredentials: signingCredentials,
            audience: _tokenOption.Audience
        );
        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);
        return new AccessToken
        {
            Token = token,
            Expiration = accessTokenExpiration
        };
    }

    private async Task<bool> checkUserExistsByEmail(string email)
    {
        var checkUserByEmail = await _userService.GetByEmailAsync(email);
        return checkUserByEmail.Success;
    }

    private bool verifyUserPassword(User user, string hashedPassword, string providedPassword)
    {
        var verifyPassword = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        return verifyPassword == PasswordVerificationResult.Failed ? false : true;
    }
}

