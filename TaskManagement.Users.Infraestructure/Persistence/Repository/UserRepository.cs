using TodoManagement.Users.Domain.Users;
using TodoManagement.Users.Domain.Users;

namespace TodoManagement.Users.Infrastructure.Persistence.Repository;

internal class UserRepository : IUserRepository
{

    public Task AddAsync(User user) => Task.CompletedTask;

    public Task<User?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetById(UserId userId)
    {
        throw new NotImplementedException();
    }
}
