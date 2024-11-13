namespace DTO;

public class CreateCommentDto
{
    public int PostId { get; set; }     // The ID of the post this comment belongs to
    public string Body { get; set; }    // The content of the comment
    public string Content { get; set; }
}