namespace Entities
{
    public class Comment
    {
        public int CommentId { get; set; }  // Unique ID for the comment
        public int PostId { get; set; }     // The ID of the post this comment belongs to
        public string Body { get; set; }    // The content of the comment
    }
}