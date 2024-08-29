using APIBusService.Core.Abstractions;
using APIBusService.Core.Entities;
using APIBusService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace APIBusService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentException(nameof(user));
        }

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> DeleteUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        _context.Users.Remove(user);

        return new User { Id = id };
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users ?? Enumerable.Empty<User>();
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        return user;
    }

    public async Task<User> UpdateUser(User user, int id)
    {
        var model = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (model == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        model.Name = user.Name;
        model.Email = user.Email;
        model.Age = user.Age;

        _context.Users.Update(model);
        await _context.SaveChangesAsync();

        return model;
    }
}
