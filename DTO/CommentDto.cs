namespace DTO;

public class CommentDto
{
    public int CommentId { get; set; }  // Unique ID for the comment
    public int PostId { get; set; }     // The ID of the post this comment belongs to
    public string Body { get; set; }    // The content of the comment
    public object Content { get; set; }
}