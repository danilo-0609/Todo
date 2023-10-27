using FluentValidation;

namespace TodoManagement.Todos.Application.Entities.Queries.GetByName.Todos;

internal sealed class GetTasksByNameQueryValidator : AbstractValidator<GetTasksByNameQuery>
{
    internal GetTasksByNameQueryValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("The name is required");
    }
}
