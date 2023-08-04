using Microsoft.AspNetCore.Http;

namespace BlogApp.Core.Utilities.Helpers.FileHelper
{
	public interface IFileHelper
	{
        Task<string> UploadImage(IFormFile file);
    }
}

