namespace DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public List<PostDto> Posts { get; set; }
        public string Password { get; set; } // Added password property
    }
}