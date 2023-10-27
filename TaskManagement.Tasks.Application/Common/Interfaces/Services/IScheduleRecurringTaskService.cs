using TodoManagement.Todos.Domain.Core.AggregateRoot;

namespace TodoManagement.Todos.Application.Common.Interfaces.Services;

public interface IScheduleRecurringTaskService
{
    void Schedule(string recurrenceType, Todo todo);
}
