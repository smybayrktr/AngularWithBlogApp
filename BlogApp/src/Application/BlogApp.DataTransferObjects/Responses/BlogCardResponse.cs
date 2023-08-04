namespace BlogApp.DataTransferObjects.Responses
{
	public class BlogCardResponse
	{
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string BookmarkImage { get; set; }
    }
}

