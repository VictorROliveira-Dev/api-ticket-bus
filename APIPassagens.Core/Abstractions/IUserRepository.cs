﻿using APIBusService.Core.Entities;

namespace APIBusService.Core.Abstractions;

public interface IUserRepository
{
    Task<User> GetUserById(int id);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> AddUser(User user);
    Task<User> UpdateUser(User user);
    Task<bool> DeleteUser(int id);
}
