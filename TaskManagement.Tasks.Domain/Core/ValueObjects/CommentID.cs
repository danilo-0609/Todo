using System.Reflection.Metadata.Ecma335;
using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Core.ValueObjects
{
    public sealed class CommentID : ValueObject
    {
        public Guid Value { get; private set; }


        private CommentID(Guid value)
        {
            Value = value;
        }

        public static CommentID Create(Guid value) => new CommentID(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
