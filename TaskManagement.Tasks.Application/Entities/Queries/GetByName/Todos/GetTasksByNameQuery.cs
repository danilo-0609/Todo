using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.AggregateRoot;

namespace TodoManagement.Todos.Application.Entities.Queries.GetByName.Todos;

public sealed record GetTasksByNameQuery(string Name) : IRequest<ErrorOr<List<Todo>>>;
