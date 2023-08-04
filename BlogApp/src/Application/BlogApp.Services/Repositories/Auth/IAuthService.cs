using BlogApp.Core.Utilities.Jwt;
using BlogApp.Core.Utilities.Results;
using BlogApp.DataTransferObjects.Requests;

namespace BlogApp.Services.Repositories.Auth
{
	public interface IAuthService
	{
        Task<IDataResult<AccessToken>> Register(UserRegisterRequest userRegisterRequest);
        Task<IDataResult<AccessToken>> Login(UserLoginRequest userLoginRequest);
       // Task<IResult> GoogleExternalResponse();
        Task<IResult> Logout();
    }
}

