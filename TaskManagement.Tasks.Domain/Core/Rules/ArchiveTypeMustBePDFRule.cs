using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Domain.Core.Rules
{
    public sealed class ArchiveTypeMustBePDFRule : IBusinessRules
    {

        private readonly Archive _archive;

        public ArchiveTypeMustBePDFRule(Archive archive)
        {
            _archive = archive;
        }

        public string Message => "The file type must be only PDF";

        public bool IsBroken()
        {
            var contentType = _archive.ArchivePDF.ContentType.ToLowerInvariant();

            if (contentType == "application/pdf")
            {
                return false;
            }

            return true;
        }
    }
}
