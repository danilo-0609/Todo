using ErrorOr;
using MediatR;
using TodoManagement.Todos.Application.Common.Interfaces.Services;
using TodoManagement.Todos.Domain.Common.Errors;
using TodoManagement.Todos.Domain.Core.AggregateRoot;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Commands.Update.Todos;

internal sealed class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, ErrorOr<Todo>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScheduleRecurringTaskService _scheduleRecurringTaskService;

    internal UpdateTodoCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork,
        IScheduleRecurringTaskService scheduleRecurringTaskService)
    {
        _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _scheduleRecurringTaskService = scheduleRecurringTaskService ?? throw new ArgumentNullException(nameof(scheduleRecurringTaskService));
    }

    public async Task<ErrorOr<Todo>>
        Handle(UpdateTodoCommand command,
        CancellationToken cancellationToken)
    {
        var todoID = TodoID.Create(command.TodoId);
        var todo = await UpdateTask(command);

        if (todo.IsError)
        {
            return todo.FirstError;
        }

        string? recurrenceType = todo.Value.RecurringTodo?.Value;

        if (recurrenceType is not null)
        {
            _scheduleRecurringTaskService.Schedule(recurrenceType, todo.Value);
        }

        await _todoRepository.UpdateTodoAsync(todo, todoID);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return todo;
    }

    private async Task<ErrorOr<Todo>> UpdateTask(UpdateTodoCommand command)
    {
        if (command.RecurringTodo is not null && (command.Tags is not null || command.Tags?.Count > 0))
        {
            var todoWithTagsAndRecurringTask = await UpdateTodoWithTagsAndRecurringTask(command);

            if (todoWithTagsAndRecurringTask.IsError)
            {
                return todoWithTagsAndRecurringTask.FirstError;
            }
        }

        if (command.RecurringTodo is not null && (command.Tags is null || command.Tags.Count == 0))
        {
            var todoWithRecurringTaskAndNoTags = await UpdateTodoWithRecurringTaskAndNoTags(command);

            if (todoWithRecurringTaskAndNoTags.IsError)
            {
                return todoWithRecurringTaskAndNoTags.FirstError;
            }

            return todoWithRecurringTaskAndNoTags;
        }

        if (command.Tags is not null && command.RecurringTodo is null)
        {
            var todoWithTagsAndNoRecurringTags = await UpdateTodoWithTagsAndNoRecurringTask(command);

            if (todoWithTagsAndNoRecurringTags.IsError)
            {
                return todoWithTagsAndNoRecurringTags.FirstError;
            }

            return todoWithTagsAndNoRecurringTags;
        }

        var todo = await UpdateTaskWithNoRecurringTaskAndTags(command);

        if (todo.IsError)
        {
            return todo.FirstError;
        }

        return todo;
    }

    private async Task<ErrorOr<Todo>> UpdateTaskWithNoRecurringTaskAndTags(UpdateTodoCommand command)
    {
        var todoID = TodoID.Create(command.TodoId);
        var currentTodo = await _todoRepository.GetTodoByIdAsync(todoID);


        if (currentTodo is null)
        {
            return Error.NotFound("Task.NotFound", "The task was not found");
        }

        var task = Todo.Update(todoID, command.Name, command.Description, DateTime.UtcNow,
                  currentTodo.CreatedDateTime);

        if (task.IsError)
        {
            return task.FirstError;
        }

        return task;
    }

    private async Task<ErrorOr<Todo>> UpdateTodoWithTagsAndRecurringTask(UpdateTodoCommand command)
    {
        var todoID = TodoID.Create(command.TodoId);
        var currentTodo = await _todoRepository.GetTodoByIdAsync(todoID);

        if (currentTodo is null)
        {
            return Error.NotFound("Todo.NotFound", "The todo was not found");
        }

        var recurringTodo = RecurringTodo.Create(command.RecurringTodo!);

        if (recurringTodo is null)
        {
            return Errors.Todos.RecurringTodoIsNotValid;
        }

        List<TodoTag> tags = command.Tags!.Select(value => TodoTag.Create(value)).ToList();

        var todoWithTagsAndRecurringTask = Todo.Update(todoID,
                    command.Name,
                    command.Description,
                    DateTime.UtcNow,
                    currentTodo.CreatedDateTime,
                    recurringTodo,
                    tags);


        if (todoWithTagsAndRecurringTask.IsError)
        {
            return todoWithTagsAndRecurringTask.FirstError;
        }

        return todoWithTagsAndRecurringTask;
    }

    private async Task<ErrorOr<Todo>> UpdateTodoWithRecurringTaskAndNoTags(UpdateTodoCommand command)
    {
        var todoID = TodoID.Create(command.TodoId);

        var currentTodo = await _todoRepository.GetTodoByIdAsync(todoID);

        if (currentTodo is null)
        {
            return Error.NotFound("Todo.NotFound", "The todo was not found");
        }

        var recurringTodo = RecurringTodo.Create(command.RecurringTodo!);

        if (recurringTodo is null)
        {
            return Errors.Todos.RecurringTodoIsNotValid;
        }

        var todoWithRecurringTaskWithNoTags = Todo.Update(todoID, command.Name,
            command.Description, DateTime.UtcNow, currentTodo.CreatedDateTime, recurringTodo);

        if (todoWithRecurringTaskWithNoTags.IsError)
        {
            return todoWithRecurringTaskWithNoTags.FirstError;
        }

        return todoWithRecurringTaskWithNoTags;
    }

    private async Task<ErrorOr<Todo>> UpdateTodoWithTagsAndNoRecurringTask(UpdateTodoCommand command)
    {
        var todoID = TodoID.Create(command.TodoId);
        var currentTodo = await _todoRepository.GetTodoByIdAsync(todoID);

        if (currentTodo is null)
        {
            return Error.NotFound("Todo.NotFound", "The todo was not found");
        }

        var tags = command.Tags!.Select(value => TodoTag.Create(value)).ToList();

        var todoWithTagsWithNoRecurringTask = Todo.Update(todoID, command.Name, command.Description,
                                DateTime.UtcNow, currentTodo.CreatedDateTime, null, tags);

        if (todoWithTagsWithNoRecurringTask.IsError)
        {
            return todoWithTagsWithNoRecurringTask.FirstError;
        }

        return todoWithTagsWithNoRecurringTask;
    }
}
