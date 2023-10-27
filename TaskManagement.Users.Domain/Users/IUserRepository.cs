namespace TodoManagement.Users.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<User?> GetById(UserId userId);

    Task<User?> GetByEmailAsync(string email);
}
