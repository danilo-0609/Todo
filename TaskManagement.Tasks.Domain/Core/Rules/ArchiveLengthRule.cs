using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Domain.Core.Rules
{
    public class ArchiveLengthRule : IBusinessRules
    {
        private const long MaximumLength = 820000;

        private readonly Archive _archive;

        public ArchiveLengthRule(Archive archive)
        {
            _archive = archive;
        }

        public string Message => "The PDF file must be shorter";

        public bool IsBroken()
        {
            if (_archive.ArchivePDF.Length > MaximumLength)
            {
                return true;
            }

            return false;
        }
    }
}
