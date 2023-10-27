using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Core.ValueObjects;

public class CompletedTodoType : ValueObject
{
    public CompletedTodoType Succesfully { get; } = new CompletedTodoType(nameof(Succesfully));
    public CompletedTodoType Good { get; } = new CompletedTodoType(nameof(Good));
    public CompletedTodoType Regular { get; } = new CompletedTodoType(nameof(Regular));
    public CompletedTodoType Poor { get; } = new CompletedTodoType(nameof(Poor));
    public CompletedTodoType NotCompleted { get; } = new CompletedTodoType(nameof(NotCompleted));

    public string Value { get; private set; }

    public CompletedTodoType(string value)
    {
        Value = value;
    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
