namespace Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
        public User Author { get; set; } 
    }
}