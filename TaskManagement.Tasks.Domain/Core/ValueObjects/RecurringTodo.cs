using ErrorOr;
using TodoManagement.Todos.Domain.Common.Errors;
using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.Rules;

namespace TodoManagement.Todos.Domain.Core.ValueObjects;

public class RecurringTodo : ValueObject
{
    public static RecurringTodo Monthly => new RecurringTodo(nameof(Monthly));

    public static RecurringTodo Weekly => new RecurringTodo(nameof(Weekly));

    public static RecurringTodo Daily => new RecurringTodo(nameof(Daily));

    public string Value { get; }

    private RecurringTodo(string value)
    {
        Value = value;
    }

    public static RecurringTodo? Create(string value)
    {
        if (ValidateCreation(value) == true)
        {
            return new RecurringTodo(value);
        }

        return null;
    }

    private static ErrorOr<bool> ValidateCreation(string value)
    {
        RecurringTodoMustBeValidRule rule = new(value);

        if (rule.IsBroken())
        {
            return Errors.Tasks.RecurringTodoNotValid(rule);
        }

        return true;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
