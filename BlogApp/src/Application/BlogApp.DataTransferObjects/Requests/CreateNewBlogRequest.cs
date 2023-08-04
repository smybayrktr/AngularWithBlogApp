namespace BlogApp.DataTransferObjects.Requests;

public class CreateNewBlogRequest
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int CategoryId { get; set; }
    public string Image { get; set; }
}

