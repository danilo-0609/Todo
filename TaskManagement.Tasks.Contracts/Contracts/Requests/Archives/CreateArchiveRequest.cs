using Microsoft.AspNetCore.Http;

namespace TodoManagement.Todos.Contracts.Contracts.Requests.Archives
{
    public sealed record CreateArchiveRequest(Guid Id, IFormFile File, string Description);
}
