using System.Net.Http.Json;
using System.Text.Json;
using BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;

    public HttpUserService(HttpClient client)
    {
        this.client = client;
    }

    // Create a new user
    public async Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    // Update an existing user
    public async Task UpdateUserAsync(int id, UpdateUserDto request)
    {
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"users/{id}", request);
        if (!httpResponse.IsSuccessStatusCode)
        {
            string response = await httpResponse.Content.ReadAsStringAsync();
            throw new Exception(response);
        }
    }

    // Retrieve a user by ID
    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await client.GetFromJsonAsync<UserDto>($"users/{id}");
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        return user;
    }

    // Retrieve all users
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await client.GetFromJsonAsync<IEnumerable<UserDto>>("users");
        if (users == null)
        {
            throw new Exception("No users found.");
        }
        return users;
    }

    // Delete a user by ID
    public async Task DeleteUserAsync(int id)
    {
        HttpResponseMessage httpResponse = await client.DeleteAsync($"users/{id}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            string response = await httpResponse.Content.ReadAsStringAsync();
            throw new Exception(response);
        }
    }
}
