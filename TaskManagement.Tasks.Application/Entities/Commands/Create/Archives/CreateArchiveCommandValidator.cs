using FluentValidation;
using TodoManagement.Todos.Domain.Core.Rules;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Commands.Create.Archives
{
    public class CreateArchiveCommandValidator : AbstractValidator<CreateArchiveCommand>
    {
        private readonly IArchiveRepository _archiveRepository;

        public CreateArchiveCommandValidator(IArchiveRepository archiveRepository)
        {
            _archiveRepository = archiveRepository;

            RuleFor(rule => rule.TodoId)
                .NotEmpty().WithMessage("The id is required");

            RuleFor(rule => rule.Name)
                .NotEmpty().WithMessage("You need to pass the name of the file")
                .MustAsync(async (name, cancellation) =>
                {
                    ArchiveMustHaveAnUniqueNameRule rule = new(_archiveRepository!, name);

                    var isUnique = await rule.NameIsUnique();

                    if (!isUnique)
                    {
                        return false;
                    }

                    return true;

                }).WithMessage("The archive's name must be unique");

            RuleFor(rule => rule.ArchivePDF)
                .NotEmpty().WithMessage("You need to pass the PDF file");
        }
    }
}
