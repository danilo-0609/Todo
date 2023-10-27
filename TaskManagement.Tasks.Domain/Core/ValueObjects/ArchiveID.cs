using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Core.ValueObjects
{
    public sealed class ArchiveID : ValueObject
    {
        public Guid Value { get; private set; }


        private ArchiveID(Guid value)
        {
            Value = value;
        }

        public static ArchiveID Create(Guid value) => new ArchiveID(value);

        public static ArchiveID CreateUnique() => new ArchiveID(Guid.NewGuid());

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private ArchiveID() { }

    }
}
