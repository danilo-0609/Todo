using TodoManagement.Users.Domain.Users;

namespace TodoManagement.Users.Application.Abstractions;

public interface IJwtGenerator
{
    string Generate(User user);
}
