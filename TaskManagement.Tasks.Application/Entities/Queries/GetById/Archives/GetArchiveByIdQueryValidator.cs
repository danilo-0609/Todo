using FluentValidation;

namespace TodoManagement.Todos.Application.Entities.Queries.GetById.Archives;

internal sealed class GetArchiveByIdQueryValidator : AbstractValidator<GetArchiveByIdQuery>
{
    internal GetArchiveByIdQueryValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("The id is required");
    }
}
