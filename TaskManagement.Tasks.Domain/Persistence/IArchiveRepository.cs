using Microsoft.AspNetCore.Http;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Core.ValueObjects;

namespace TodoManagement.Todos.Domain.Persistence
{
    public interface IArchiveRepository
    {
        Task<Archive?> GetArchiveByIdAsync(ArchiveID archiveId);

        Task<Archive?> GetArchivesByName(string name);

        Task<List<Archive>> GetAllArchives();

        Task AddArchiveAsync(IFormFile file);

        Task DeleteArchiveAsync(ArchiveID archiveId);

        Task<bool> ArchiveNameIsUnique(string name);
    }
}
