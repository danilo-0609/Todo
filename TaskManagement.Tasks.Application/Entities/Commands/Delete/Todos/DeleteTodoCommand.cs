using ErrorOr;
using MediatR;

namespace TodoManagement.Todos.Application.Entities.Commands.Delete.Todos;

public sealed record DeleteTodoCommand(Guid TodoId) : IRequest<ErrorOr<Unit>>;
