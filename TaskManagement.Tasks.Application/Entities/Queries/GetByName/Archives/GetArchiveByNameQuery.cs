using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Application.Entities.Queries.GetByName.Archives;

public sealed record GetArchiveByNameQuery(string Name) : IRequest<ErrorOr<Archive>>;
