using ErrorOr;
using TodoManagement.Todos.Domain.Common.Errors;
using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Core.Events;
using TodoManagement.Todos.Domain.Core.Rules;
using TodoManagement.Todos.Domain.Core.ValueObjects;

namespace TodoManagement.Todos.Domain.Core.AggregateRoot;

public sealed class Todo : AggregateRoot<TodoID, Guid>
{
    private readonly List<TodoTag> _tags = new();
    private readonly List<Archive>? _archives = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public RecurringTodo? RecurringTodo { get; private set; }
    public Comment? Comment { get; private set; }


    public IReadOnlyList<TodoTag> TodoTags => _tags.AsReadOnly();
    public IReadOnlyList<Archive>? Archives => _archives.AsReadOnly();

    public bool IsActive { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }

    private Todo(
        TodoID todoId,
        string name,
        string description,
        RecurringTodo? recurringTodo,
        List<TodoTag> tags,
        Comment? comment,
        List<Archive>? archives)
        : base(todoId)
    {
        Name = name;
        Description = description;
        RecurringTodo = recurringTodo;
        _tags = tags;
        Comment = comment;
        _archives = archives;

        IsActive = true;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = null;
    }

    private Todo(
        TodoID todoId,
        string name,
        string description,
        RecurringTodo? recurringTodo,
        List<TodoTag> tags,
        Comment? comment,
        List<Archive>? archives,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(todoId)
    {
        Name = name;
        Description = description;
        RecurringTodo = recurringTodo;
        _tags = tags;
        Comment = comment;
        _archives = archives;

        IsActive = true;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }


    public static ErrorOr<Todo> Create(
        string name,
        string description,
        RecurringTodo? recurringTodo = null,
        List<TodoTag>? tags = null,
        Comment? comment = null,
        List<Archive>? archives = null
        )
    {
        var todo = new Todo(TodoID.CreateUnique(), name, description,
            recurringTodo, tags ?? new(), comment,
            archives ?? new());

        NameLengthRule nameLengthRule = new(name);

        if (nameLengthRule.IsBroken())
        {
            return Errors.Todos.NameLengthGreaterThanRequired(nameLengthRule);
        }

        DescriptionLengthRule descriptionLengthRule = new(description);

        if (descriptionLengthRule.IsBroken())
        {
            return Errors.Todos.DescriptionLengthGreaterThanRequired(descriptionLengthRule);
        }

        todo.AddDomainEvent(new TodoCreatedEvent(todo));

        return todo;
    }

    public static ErrorOr<Todo> Update(
        TodoID taskId,
        string name,
        string description,
        DateTime updatedDateTime,
        DateTime createdDateTime,
        RecurringTodo? recurringTodo = null,
        List<TodoTag>? tags = null,
        Comment? comment = null,
        List<Archive>? archives = null)
    {
        var todo = new Todo(taskId, name, description, recurringTodo, tags ?? new(),
            comment, archives ?? new(), createdDateTime, updatedDateTime);

        NameLengthRule nameLengthRule = new(name);

        if (nameLengthRule.IsBroken())
        {
            return Errors.Todos.NameLengthGreaterThanRequired(nameLengthRule);
        }

        DescriptionLengthRule descriptionLengthRule = new(description);

        if (descriptionLengthRule.IsBroken())
        {
            return Errors.Todos.DescriptionLengthGreaterThanRequired(descriptionLengthRule);
        }

        todo.AddDomainEvent(new TodoUpdatedEvent(todo));

        return todo;
    }

    public static ErrorOr<Todo> CreateScheduleTodo(Guid id,
        string name,
        string description,
        RecurringTodo? recurringTodo = null,
        List<TodoTag>? tags = null,
        Comment? comment = null,
        List<Archive>? archives = null)
    {
        var todo = new Todo(TodoID.Create(id), name, description,
            recurringTodo, tags ?? new(), comment,
            archives ?? new());

        NameLengthRule nameLengthRule = new(name);

        if (nameLengthRule.IsBroken())
        {
            return Errors.Todos.NameLengthGreaterThanRequired(nameLengthRule);
        }

        DescriptionLengthRule descriptionLengthRule = new(description);

        if (descriptionLengthRule.IsBroken())
        {
            return Errors.Todos.DescriptionLengthGreaterThanRequired(descriptionLengthRule);
        }

        todo.AddDomainEvent(new TodoScheduledEvent(todo));

        return todo;
    }
}



