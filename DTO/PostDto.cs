using System.Text.Json.Serialization;

namespace DTO
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}