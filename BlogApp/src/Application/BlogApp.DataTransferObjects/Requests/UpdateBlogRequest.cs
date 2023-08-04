using Microsoft.AspNetCore.Http;

namespace BlogApp.DataTransferObjects.Requests;

public class UpdateBlogRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Image { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
}


