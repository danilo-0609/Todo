using FluentValidation;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Core.Rules;

namespace TodoManagement.Todos.Application.Entities.Commands.Update.Comments;

internal sealed class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    internal UpdateCommentCommandValidator()
    {
        RuleFor(rule => rule.Comment)
            .NotEmpty().WithMessage("Comment must not be empty")
            .Must(value =>
            {
                var comment = Comment.CreateComment(value);

                CommentLengthRule commentLengthRule = new(comment.Value);

                if (commentLengthRule.IsBroken())
                {
                    return false;
                }

                return true;

            }).WithMessage("The comment must not be greater than 250 characters");

        RuleFor(rule => rule.TodoId)
            .NotEmpty().WithMessage("The id of the todo is required");

        RuleFor(rule => rule.CommentId)
            .NotEmpty().WithMessage("Te id of the comment is required");
    }
}
