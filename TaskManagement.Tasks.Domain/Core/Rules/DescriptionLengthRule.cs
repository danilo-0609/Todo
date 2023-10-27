using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Core.Rules
{
    public sealed class DescriptionLengthRule : IBusinessRules
    {
        private const int MaximumLength = 4000;

        private readonly string _description;

        public DescriptionLengthRule(string description)
        {
            _description = description;
        }

        public string Message => "The description must be shorter than 4000 words";

        public bool IsBroken()
        {
            if (_description.Length > MaximumLength)
            {
                return true;
            }

            return false;
        }
    }
}
