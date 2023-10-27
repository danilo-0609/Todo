using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.AggregateRoot;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Queries.GetById.Todos;

internal sealed class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, ErrorOr<Todo>>
{
    private readonly ITodoRepository _taskRepository;

    internal GetTaskByIdQueryHandler(ITodoRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<Todo>> Handle
        (GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        var taskID = TodoID.Create(query.Id);

        if (await _taskRepository.GetTodoByIdAsync(taskID) is not Todo task)
        {
            return Error.NotFound("Task.NotFound", "The task was not found");
        }

        return task!;
    }
}
