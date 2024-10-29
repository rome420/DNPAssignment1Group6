using System.Text.Json.Serialization;

namespace DTO
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        [JsonIgnore] // Prevent serialization to avoid circular references
        public String Author { get; set; }
    }
}