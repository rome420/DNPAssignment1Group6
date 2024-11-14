namespace Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public User Author { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; } 
        public string Body { get; set; } 
    }
}