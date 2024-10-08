﻿using System.Collections;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private readonly List<Post> _posts = new List<Post>();

    public async Task<Post> AddAsync(Post post)
    {
        if (post == null)
        {
            throw new ArgumentNullException(nameof(post));
        }

        post.PostId = _posts.Any() 
            ? _posts.Max(p => p.PostId) + 1
            : 1;
        _posts.Add(post);
        return await Task.FromResult(post);
    }

    public async Task UpdateAsync(Post post)
    {
        if (post == null)
        {
            throw new ArgumentNullException(nameof(post));
        }

        var existingPost = _posts.SingleOrDefault(p => p.PostId == post.PostId);
        if (existingPost == null)
        {
            throw new InvalidOperationException($"Post with ID '{post.PostId}' not found.");
        }

        _posts.Remove(existingPost);
        _posts.Add(post);

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var postToRemove = _posts.SingleOrDefault(p => p.PostId == id);
        if (postToRemove == null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found.");
        }

        _posts.Remove(postToRemove);
        await Task.CompletedTask;
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        var post = _posts.SingleOrDefault(p => p.PostId == id);
        if (post == null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found.");
        }

        return await Task.FromResult(post);
    }

    public IQueryable<Post> GetMany()
    {
        return _posts.AsQueryable();
    }

    // Implementing GetPostByIdAsync method to satisfy the interface contract
    public Task<Post> GetPostByIdAsync(int postId)
    {
        var post = _posts.SingleOrDefault(p => p.PostId == postId);
        return Task.FromResult(post);
    }
    
    public Task<IEnumerable<Post>> GetPostsAsync()
    {
        return Task.FromResult(_posts.AsEnumerable());
    }
    
    public PostInMemoryRepository()
    {
        // Adding dummy posts
        _posts.Add(new Post { PostId = 1, Title = "First Post", Body = "This is the first post" });
        _posts.Add(new Post { PostId = 2, Title = "Second Post", Body = "This is the second post" });
        _posts.Add(new Post { PostId = 3, Title = "Third Post", Body = "This is the third post" });
    }

}