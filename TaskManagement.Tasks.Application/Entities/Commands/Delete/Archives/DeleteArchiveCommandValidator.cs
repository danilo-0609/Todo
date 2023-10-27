using FluentValidation;

namespace TodoManagement.Todos.Application.Entities.Commands.Delete.Archives;

internal sealed class DeleteArchiveCommandValidator : AbstractValidator<DeleteArchiveCommand>
{
    internal DeleteArchiveCommandValidator()
    {
        RuleFor(r => r.ArchiveID)
            .NotEmpty().WithMessage("The id of the archive is required");

        RuleFor(r => r.TodoId)
            .NotEmpty().WithMessage("The id of the task is required");
    }
}
