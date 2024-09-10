using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Post Add(Post post);
    void Update(Post post);
    void Delete(int id);
    Post GetSingle(int id);
    IQueryable<Post> GetMany();
}