using ErrorOr;
using MediatR;
using TodoManagement.Todos.Application.Common.Interfaces.Services;
using TodoManagement.Todos.Domain.Common.Errors;
using TodoManagement.Todos.Domain.Core.AggregateRoot;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Commands.Create.Todos;

internal sealed class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, ErrorOr<Todo>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScheduleRecurringTaskService _scheduleRecurringTaskService;

    internal CreateTodoCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork,
        IScheduleRecurringTaskService scheduleRecurringTaskService)
    {
        _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _scheduleRecurringTaskService = scheduleRecurringTaskService ?? throw new ArgumentNullException(nameof(scheduleRecurringTaskService));
    }

    public async Task<ErrorOr<Todo>> Handle(
        CreateTodoCommand command,
        CancellationToken cancellationToken)
    {
        var todo = CreateTodo(command);

        if (todo.IsError)
        {
            return todo.FirstError;
        }

        string? recurrenceType = todo.Value.RecurringTodo?.Value;

        if (recurrenceType is not null)
        {
            _scheduleRecurringTaskService.Schedule(recurrenceType, todo.Value);
        }

        await _todoRepository.AddTodoAsync(todo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return todo;
    }

    private ErrorOr<Todo> CreateTodo(CreateTodoCommand command)
    {
        if (command.RecurringTodo is not null && (command.Tags is not null || command.Tags?.Count > 0))
        {
            var todoWithTagsAndRecurringTask = CreateTodoWithTagsAndRecurringTask(command);

            if (todoWithTagsAndRecurringTask.IsError)
            {
                return todoWithTagsAndRecurringTask.FirstError;
            }

            return todoWithTagsAndRecurringTask;
        }

        if (command.RecurringTodo is not null && (command.Tags is null || command.Tags.Count == 0))
        {
            var todoWithRecurringTaskAndNoTags = CreateTodoWithRecurringTaskAndNoTags(command);

            if (todoWithRecurringTaskAndNoTags.IsError)
            {
                return todoWithRecurringTaskAndNoTags.FirstError;
            }

            return todoWithRecurringTaskAndNoTags;
        }

        if (command.Tags is not null && command.RecurringTodo is null)
        {
            var todoWithTagsAndNoRecurringTags = CreateTodoWithTagsAndNoRecurringTask(command);

            if (todoWithTagsAndNoRecurringTags.IsError)
            {
                return todoWithTagsAndNoRecurringTags.FirstError;
            }

            return todoWithTagsAndNoRecurringTags;
        }

        var todo = CreateTodoWithNoRecurringTaskAndTags(command);

        if (todo.IsError)
        {
            return todo.FirstError;
        }

        return todo;
    }

    private ErrorOr<Todo> CreateTodoWithTagsAndRecurringTask(CreateTodoCommand command)
    {
        var recurringTodo = RecurringTodo.Create(command.RecurringTodo!);

        if (recurringTodo is null)
        {
            return Errors.Todos.RecurringTodoIsNotValid;
        }

        List<TodoTag> tags = command.Tags!.Select(value => TodoTag.Create(value)).ToList();

        var todoWithTagsAndRecurringTask = Todo.Create(command.Name,
                command.Description,
                recurringTodo,
                tags);

        if (todoWithTagsAndRecurringTask.IsError)
        {
            return todoWithTagsAndRecurringTask.FirstError;
        }

        return todoWithTagsAndRecurringTask;
    }

    private ErrorOr<Todo> CreateTodoWithRecurringTaskAndNoTags(CreateTodoCommand command)
    {
        var recurringTodo = RecurringTodo.Create(command.RecurringTodo!);

        if (recurringTodo is null)
        {
            return Errors.Todos.RecurringTodoIsNotValid;
        }

        var todoWithRecurringTaskWithNoTags = Todo.Create(command.Name,
                command.Description,
                recurringTodo);

        return todoWithRecurringTaskWithNoTags;
    }

    private ErrorOr<Todo> CreateTodoWithTagsAndNoRecurringTask(CreateTodoCommand command)
    {
        var tags = command.Tags!.Select(value => TodoTag.Create(value)).ToList();


        var todoWithTagsWithNoRecurringTask = Todo.Create(command.Name, command.Description, null, tags);

        if (todoWithTagsWithNoRecurringTask.IsError)
        {
            return todoWithTagsWithNoRecurringTask.FirstError;
        }

        return todoWithTagsWithNoRecurringTask;
    }

    private ErrorOr<Todo> CreateTodoWithNoRecurringTaskAndTags(CreateTodoCommand command)
    {
        var todo = Todo.Create(command.Name, command.Description);

        if (todo.IsError)
        {
            return todo.FirstError;
        }

        return todo;
    }
}
