namespace Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }  // Ensure this property is defined
        public User Author { get; set; }  // Ensure this property is defined
        public List<Comment> Comments { get; set; } = new();
    }
}