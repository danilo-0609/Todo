using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.ValueObjects;

namespace TodoManagement.Todos.Domain.Core.Rules
{
    public sealed class TagsLengthRule : IBusinessRules
    {
        private readonly List<TodoTag> _tags;

        public TagsLengthRule(List<TodoTag> tags)
        {
            _tags = tags;
        }

        public string Message => "The tag length must be greater than 3 words and shorter than 30 words";

        public bool IsBroken()
        {
            if (_tags.Any(tag => tag.Value.Length > 30 || tag.Value.Length < 3))
            {
                return true;
            }

            return false;
        }
    }
}
