using ErrorOr;
using Microsoft.AspNetCore.Http;
using TodoManagement.Todos.Domain.Common.Errors;
using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.Events;
using TodoManagement.Todos.Domain.Core.Rules;
using TodoManagement.Todos.Domain.Core.ValueObjects;

namespace TodoManagement.Todos.Domain.Core.Entities
{
    public sealed class Archive : Entity<ArchiveID>
    {
        public string Name { get; private set; }

        public IFormFile ArchivePDF { get; private set; }

        private Archive(ArchiveID archiveId, IFormFile archivePDF, string name)
            : base(archiveId)
        {
            ArchivePDF = archivePDF;
            Name = name;
        }

        public static ErrorOr<Archive> Create(IFormFile formFile, string name)
        {
            var archive = new Archive(ArchiveID.Create(Guid.NewGuid()), formFile, name);

            ArchiveTypeMustBePDFRule fileTypeMustBePDFRule = new(archive);

            if (fileTypeMustBePDFRule.IsBroken())
            {
                return Errors.Tasks.FileContentTypeMustBePDF(fileTypeMustBePDFRule);
            }

            ArchiveLengthRule fileLengthRule = new(archive);

            if (fileLengthRule.IsBroken())
            {
                return Errors.Tasks.FileLengthGreaterThanRequired(fileLengthRule);
            }

            archive.AddDomainEvent(new ArchiveCreatedEvent(archive));

            return archive;
        }
    }
}
