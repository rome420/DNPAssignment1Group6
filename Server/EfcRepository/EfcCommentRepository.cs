using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfcRepository
{
    public class EfcCommentRepository : ICommentRepository
    {
        private readonly AppContext ctx;

        public EfcCommentRepository(AppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            await ctx.Comments.AddAsync(comment);
            await ctx.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteAsync(int commentId)
        {
            var comment = await ctx.Comments.FindAsync(commentId);
            if (comment != null)
            {
                ctx.Comments.Remove(comment);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            return await ctx.Comments
                .Include(c => c.Author)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.CommentId == commentId);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await ctx.Comments
                .Include(c => c.Author)
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            ctx.Comments.Update(comment);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync()
        {
            return await ctx.Comments
                .Include(c => c.Author)
                .Include(c => c.Post)
                .ToListAsync();
        }

        public IQueryable<Comment> GetMany()
        {
            return ctx.Comments.AsQueryable();
        }

        public async Task<Comment?> GetSingleAsync(int commentId)
        {
            return await ctx.Comments.SingleOrDefaultAsync(c => c.CommentId == commentId);
        }
    }
}