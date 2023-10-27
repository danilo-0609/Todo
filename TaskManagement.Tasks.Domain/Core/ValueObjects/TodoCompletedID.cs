using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Core.ValueObjects
{
    public sealed class TodoCompletedID : AggregateRootID<Guid>
    {
        public override Guid Value { get; protected set; }

        private TodoCompletedID(Guid value)
        {
            Value = value;
        }

        public static TodoCompletedID Create(Guid value) => new TodoCompletedID(value);

        public static TodoCompletedID CreateUnique() => new TodoCompletedID(Guid.NewGuid());

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private TodoCompletedID() { }
    }
}
