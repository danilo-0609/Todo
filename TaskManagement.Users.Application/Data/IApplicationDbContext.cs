using Microsoft.EntityFrameworkCore;
using TodoManagement.Users.Domain.Users;

namespace TodoManagement.Users.Application.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
