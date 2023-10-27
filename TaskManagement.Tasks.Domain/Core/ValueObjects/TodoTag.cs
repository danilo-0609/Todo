using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Core.ValueObjects
{
    public sealed class TodoTag : ValueObject
    {

        public string Value { get; private set; }

        private TodoTag(string value)
        {
            Value = value;
        }

        public static TodoTag Create(string value) => new TodoTag(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
