using ErrorOr;
using MediatR;
using TodoManagement.Todos.Application.Common.Interfaces.Services;
using TodoManagement.Todos.Domain.Core.AggregateRoot;

namespace TodoManagement.Todos.Infrastructure.Services.SchedulingTasks;

internal class ScheduleRecurringTaskService : IScheduleRecurringTaskService
{
    private const double DailyInterval = 86400000;
    private const double WeeklyInterval = 604800000;
    private const double MontlyInterval = 2592000000;

    public void Schedule(string recurrenceType, Todo todo)
    {
        if (recurrenceType == "Daily")
        {
            CreateTodoPeriodically(todo, DailyInterval);
        }
         
        if (recurrenceType == "Weekly")
        {
            CreateTodoPeriodically(todo, WeeklyInterval);
        }

        if (recurrenceType == "Monthly")
        {
            CreateTodoPeriodically(todo, MontlyInterval);
        }
    }

    private static void CreateTodoPeriodically(Todo todo, double interval)
    {
        System.Timers.Timer timer = new(interval);

        timer.Elapsed += (sender, e) =>
        {
            CreateTodo(todo);
        };

        timer.Start();
    } 

    private static ErrorOr<Unit> CreateTodo(Todo todo)
    {
        var todoCreated = Todo.CreateScheduleTodo(todo.Id.Value, todo.Name, todo.Description,
        todo.RecurringTodo, todo.TodoTags.ToList(), todo.Comment);


        if (todoCreated.IsError)
        {
            return todoCreated.FirstError;
        }

        return Unit.Value;
    }
}
