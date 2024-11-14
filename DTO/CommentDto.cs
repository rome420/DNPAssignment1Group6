namespace DTO
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }
        public string Author { get; set; }
    }
}