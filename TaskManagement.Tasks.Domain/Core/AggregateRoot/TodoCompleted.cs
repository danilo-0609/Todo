using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.Events;
using TodoManagement.Todos.Domain.Core.ValueObjects;

namespace TodoManagement.Todos.Domain.Core.AggregateRoot;

public sealed class TodoCompleted : AggregateRoot<TodoCompletedID, Guid>
{
    public TodoID TaskId { get; private set; }

    public bool IsRecurringTask { get; private set; }

    public DateTime CompletedDateTime { get; private set; }

    public CompletedTodoType CompletedTaskType { get; private set; }

    private TodoCompleted(TodoCompletedID taskCompletedId,
                          TodoID taskId,
                          bool isRecurringTask,
                          DateTime completedDateTime,
                          CompletedTodoType completedTaskType)
                           : base(taskCompletedId)
    {
        TaskId = taskId;
        IsRecurringTask = isRecurringTask;
        CompletedDateTime = completedDateTime;
        CompletedTaskType = completedTaskType;
    }


    public static TodoCompleted Complete(TodoID taskId,
                          bool isRecurringTask,
                          DateTime completedDateTime,
                          CompletedTodoType completedTaskType)
    {
        var taskCompleted = new TodoCompleted(TodoCompletedID.CreateUnique(), taskId, isRecurringTask,
                                                completedDateTime, completedTaskType);

        taskCompleted.AddDomainEvent(new TodoCompletedEvent(taskCompleted));

        return taskCompleted;
    }
}
