using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepository;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]"); 
        }
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
 
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) ?? new List<Comment>();

        
        int maxId = comments.Count > 0 ? comments.Max(c => c.CommentId) : 0;
        comment.CommentId = maxId + 1;
        comments.Add(comment);


        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        
        return comment;
    }

    public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
    {
  
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) ?? new List<Comment>();

     
        return comments.Where(c => c.PostId == postId);
    }

    public async Task UpdateAsync(Comment comment)
    {
  
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) ?? new List<Comment>();


        var existingComment = comments.SingleOrDefault(c => c.CommentId == comment.CommentId);
        if (existingComment == null)
        {
            throw new InvalidOperationException($"Comment with ID {comment.CommentId} not found.");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

       
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public async Task DeleteAsync(int id)
    {

        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) ?? new List<Comment>();

      
        var commentToRemove = comments.SingleOrDefault(c => c.CommentId == id);
        if (commentToRemove == null)
        {
            throw new InvalidOperationException($"Comment with ID {id} not found.");
        }

        comments.Remove(commentToRemove);

        
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public async Task<Comment?> GetSingleAsync(int id)
    {

        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) ?? new List<Comment>();


        return comments.SingleOrDefault(c => c.CommentId == id);
    }

    public IQueryable<Comment> GetMany()
    {

        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) ?? new List<Comment>();

        return comments.AsQueryable();
    }
}