using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfcRepository
{
    public class EfcUserRepository : IUserRepository
    {
        private readonly AppContext ctx;

        public EfcUserRepository(AppContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<User> AddAsync(User user)
        {
            await ctx.Users.AddAsync(user);
            await ctx.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await ctx.Users.FindAsync(userId);
            if (user != null)
            {
                ctx.Users.Remove(user);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await ctx.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task UpdateAsync(User user)
        {
            ctx.Users.Update(user);
            await ctx.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            return await ctx.Users.FindAsync(userId);
        }

        public IQueryable<User> GetMany()
        {
            return ctx.Users.AsQueryable();
        }

        public async Task<User?> GetSingleAsync(int userId)
        {
            return await ctx.Users.SingleOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await ctx.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await ctx.Users.ToListAsync();
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await ctx.Users.AnyAsync(u => u.Username == username);
        }
    }
}