using ErrorOr;
using MediatR;

namespace TodoManagement.Todos.Application.Entities.Commands.Delete.Archives;

public sealed record DeleteArchiveCommand(Guid ArchiveID, Guid TodoId) : IRequest<ErrorOr<Unit>>;
