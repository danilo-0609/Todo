using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Application.Entities.Queries.GetById.Archives;

public sealed record GetArchiveByIdQuery(Guid Id) : IRequest<ErrorOr<Archive>>;
