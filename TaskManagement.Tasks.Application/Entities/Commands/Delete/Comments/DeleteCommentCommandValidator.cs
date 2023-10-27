using FluentValidation;

namespace TodoManagement.Todos.Application.Entities.Commands.Delete.Comments;

internal sealed class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    internal DeleteCommentCommandValidator()
    {
        RuleFor(r => r.CommentId)
            .NotEmpty().WithMessage("The id of the comment is required");

        RuleFor(r => r.TodoId)
            .NotEmpty().WithMessage("The id of the task is required");
    }
}
