using FluentValidation;

namespace TodoManagement.Todos.Application.Entities.Commands.Delete.Todos;

internal sealed class DeleteTaskCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    internal DeleteTaskCommandValidator()
    {
        RuleFor(r => r.TodoId)
            .NotEmpty().WithMessage("The task id is required");
    }
}
