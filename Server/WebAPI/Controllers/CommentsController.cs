using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;


    public CommentsController(ICommentRepository commentRepository)
    {
        this._commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<ActionResult<CommentDto>> GetComments()
    {
        var comments = _commentRepository.GetMany().ToList();
        var dtos = new List<CommentDto>();
        foreach (var comment in comments)
        {
            dtos.Add(new CommentDto()
            {
                Body = comment.Body,
                CommentId = comment.CommentId, 
                PostId = comment.PostId
            });
            
        }


        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> GetComments([FromBody] CreateCommentDto request)
    {
        var comment = new Comment()
        {
            Body = request.Body,
            CommentId = 2137,
            PostId = request.PostId

        };

        var addedComment = await _commentRepository.AddAsync(comment);
        return Created($"/Comments/{addedComment.CommentId}", addedComment);
    }
    
    
    [HttpGet("{id:int}")]
    
    public async Task<ActionResult<CommentDto>> GetSingleComment([FromRoute] int id)
    {

        var comment = await _commentRepository.GetSingleAsync(id);


        return Ok(new CommentDto()
        {
            Body = comment.Body,
            CommentId = comment.CommentId,
            PostId = comment.PostId
        });
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult<CommentDto>> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto request)
    {
        var existingComment = await _commentRepository.GetSingleAsync(id);
        if (existingComment == null)
        {
            return NotFound(); 
        }
        
        existingComment.Body = request.Body;
        existingComment.PostId = request.PostId;
        
        await _commentRepository.UpdateAsync(existingComment); 
        
        var updatedComment = await _commentRepository.GetSingleAsync(id);
        
        return Ok(new CommentDto
        {
            CommentId = updatedComment.CommentId,
            Body = updatedComment.Body,
            PostId = updatedComment.PostId
        });
    }
    
    



}


