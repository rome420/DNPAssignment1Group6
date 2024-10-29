using System.Text.Json.Serialization;

namespace Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    

    [JsonIgnore] // Prevents serialization of the Posts property
    public List<Post> Posts { get; set; }
}
