using Entities;
using RepositoryContracts;
namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    public User Add(User user)
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public User GetSingle(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> GetMany()
    {
        throw new NotImplementedException();
    }
}