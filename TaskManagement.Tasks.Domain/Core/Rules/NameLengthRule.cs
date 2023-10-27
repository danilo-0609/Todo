using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Core.Rules
{
    public sealed class NameLengthRule : IBusinessRules
    {
        private const int MaximunLength = 120;

        private readonly string _name;

        public NameLengthRule(string name)
        {
            _name = name;
        }

        public string Message => "The task name must be shorter than 120 words";

        public bool IsBroken()
        {
            if (_name.Length > MaximunLength)
            {
                return true;
            }

            return false;
        }
    }
}
