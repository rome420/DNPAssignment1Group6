using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using DTO;

namespace BlazorApp.Auth
{
    public class SimpleAuthProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public SimpleAuthProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Login(string username, string password)
        {
            var loginRequest = new LoginRequest(username, password);
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var userDto = await response.Content.ReadFromJsonAsync<UserDto>();
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userDto.Username),
                    new Claim(ClaimTypes.NameIdentifier, userDto.UserId.ToString())
                }, "apiauth");

                var user = new ClaimsPrincipal(identity);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            }
            else
            {
                throw new Exception("Login failed: " + response.ReasonPhrase);
            }
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}