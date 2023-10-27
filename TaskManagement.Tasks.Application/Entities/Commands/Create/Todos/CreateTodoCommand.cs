using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.AggregateRoot;

namespace TodoManagement.Todos.Application.Entities.Commands.Create.Todos
{
    public record CreateTodoCommand(string Name, string Description, string? RecurringTodo = null,
                                    List<string>? Tags = null) : IRequest<ErrorOr<Todo>>;
}