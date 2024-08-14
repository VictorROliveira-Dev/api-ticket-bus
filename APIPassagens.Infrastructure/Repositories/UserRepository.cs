using APIBusService.Core.Abstractions;
using APIBusService.Core.Entities;

namespace APIBusService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task<User> AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}
