using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Core.ValueObjects
{
    public sealed class TodoID : AggregateRootID<Guid>
    {
        public override Guid Value { get; protected set; }

        private TodoID(Guid value)
        {
            Value = value;
        }

        public static TodoID Create(Guid value) => new TodoID(value);

        public static TodoID CreateUnique() => new TodoID(Guid.NewGuid());

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private TodoID() { }
    }
}
