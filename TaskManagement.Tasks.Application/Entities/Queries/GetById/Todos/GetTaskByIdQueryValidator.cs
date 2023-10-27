using FluentValidation;

namespace TodoManagement.Todos.Application.Entities.Queries.GetById.Todos;

internal sealed class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
{
    internal GetTaskByIdQueryValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("The id is required");
    }
}

