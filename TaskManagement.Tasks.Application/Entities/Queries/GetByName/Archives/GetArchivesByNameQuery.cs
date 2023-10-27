using FluentValidation;

namespace TodoManagement.Todos.Application.Entities.Queries.GetByName.Archives;

internal sealed class GetArchivesByNameQuery : AbstractValidator<GetArchiveByNameQuery>
{
    internal GetArchivesByNameQuery()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("The name is required");
    }
}
