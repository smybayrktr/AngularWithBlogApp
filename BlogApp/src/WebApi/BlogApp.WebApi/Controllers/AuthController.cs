using BlogApp.Core.Utilities.Jwt;
using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Requests;
using BlogApp.Services.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IDataResult<AccessToken>> Login(UserLoginRequest userLoginRequest)
    {
        return await _authService.Login(userLoginRequest);
    }

    [HttpPost("register")]
    public async Task<IDataResult<AccessToken>> Register(UserRegisterRequest userRegisterRequest)
    {
        return await _authService.Register(userRegisterRequest);
    }

    //[HttpPost("google-login")]
    //public async Task<IResult> GoogleLogin()
    //{
    //    return await _authService.GoogleExternalResponse();
    //}
}


