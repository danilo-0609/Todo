using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Core.Rules;

public sealed class RecurringTodoMustBeValidRule : IBusinessRules
{
    private readonly string _recurringTask;

    public RecurringTodoMustBeValidRule(string recurringTask)
    {
        _recurringTask = recurringTask;
    }

    public string Message => "The recurring task must be just a valid value, whether it is montly, " +
        "weekly or daily";

    public bool IsBroken()
    {
        if (!Validate())
        {
            return true;
        }

        return false;
    }

    private bool Validate()
    {
        if (_recurringTask == "Montly" || _recurringTask == "Weekly" ||
            _recurringTask == "Daily")
        {
            return true;
        }

        return false;
    }
}
