using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Core.Utilities.Helpers.FileHelper
{
	public class FileHelper:IFileHelper
	{
        private IHostingEnvironment _hostEnvironment;

        public FileHelper(IHostingEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            string uniqueFileName = "";
            if (file != null)
            {
                string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "images/uploaded-images");
                uniqueFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return "images/uploaded-images/" + uniqueFileName;
            }
            return "";
        }
    }
}

