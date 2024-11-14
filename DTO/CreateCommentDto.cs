namespace DTO
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Author { get; set; }
    }
}