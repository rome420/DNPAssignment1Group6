using Entities;
using RepositoryContracts;
namespace InMemoryRepositories;

public class CommentInMemoryRepository :ICommentRepository
{
    public Comment Add(Comment comment)
    {
        throw new NotImplementedException();
    }

    public void Update(Comment comment)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Comment GetSingle(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Comment> GetMany()
    {
        throw new NotImplementedException();
    }
}