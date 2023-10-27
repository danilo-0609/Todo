using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.AggregateRoot;

namespace TodoManagement.Todos.Application.Entities.Queries.GetById.Todos;

public record GetTaskByIdQuery(Guid Id) : IRequest<ErrorOr<Todo>>;
