using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Application.Entities.Commands.Create.Archives
{
    public record CreateArchiveCommand(Guid TodoId, IFormFile ArchivePDF, string Name) : IRequest<ErrorOr<Archive>>;
}
