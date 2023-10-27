using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.AggregateRoot;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Queries.GetByName.Todos;

internal sealed class GetTasksByNameQueryHandler : IRequestHandler<GetTasksByNameQuery, ErrorOr<List<Todo>>>
{
    public readonly ITodoRepository _taskRepository;

    internal GetTasksByNameQueryHandler(ITodoRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<List<Todo>>> Handle(GetTasksByNameQuery query, CancellationToken cancellationToken)
    {
        string name = query.Name;

        if (await _taskRepository.GetTodoByNameAsync(name) is not List<Todo> task)
        {
            return Error.NotFound("Todo.NotFound", $"The todo with the name {name} was not found");
        }


        return task!;
    }
}
