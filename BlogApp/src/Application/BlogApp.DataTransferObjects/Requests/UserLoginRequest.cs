using System;
namespace BlogApp.DataTransferObjects.Requests
{
	public class UserLoginRequest
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}

