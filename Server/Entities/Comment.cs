namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Body { get; set; }


    public Post Post { get; set; }
}