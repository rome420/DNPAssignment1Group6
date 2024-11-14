using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DTO;

namespace BlazorApp.Services
{
    public class HttpPostService : IPostService
    {
        private readonly HttpClient _httpClient;

        public HttpPostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _httpClient.GetFromJsonAsync<IEnumerable<PostDto>>("api/posts");
            return posts ?? new List<PostDto>();
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _httpClient.GetFromJsonAsync<PostDto>($"api/posts/{id}");
            return post ?? new PostDto();
        }

        public async Task AddPostAsync(CreatePostDto post)
        {
            var response = await _httpClient.PostAsJsonAsync("api/posts", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdatePostAsync(int id, UpdatePostDto post)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/posts/{id}", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePostAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/posts/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task AddCommentAsync(int postId, CreateCommentDto createCommentDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/posts/{postId}/comments", createCommentDto);
            response.EnsureSuccessStatusCode();
        }
    }
}