namespace BlogApp.Entities
{
	public class SavedBlog: IEntity
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public int BlogId { get; set; }
	}
}

