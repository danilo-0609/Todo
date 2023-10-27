using Microsoft.EntityFrameworkCore;
using TodoManagement.Todos.Domain.Core.AggregateRoot;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Todo> Task { get; set; }

        DbSet<Comment> Comment { get; set; }

        DbSet<Archive> Archive { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
