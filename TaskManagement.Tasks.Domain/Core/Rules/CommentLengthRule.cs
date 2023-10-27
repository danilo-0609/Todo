using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Domain.Core.Rules;

public sealed class CommentLengthRule : IBusinessRules
{
    private const int MaximumLength = 320;

    private readonly Comment _comment;

    public CommentLengthRule(Comment comment)
    {
        _comment = comment;
    }

    public string Message => "The comment must not be greater than 250 characters";

    public bool IsBroken()
    {
        if (_comment.Value.Length > 320)
        {
            return true;
        }

        return false;
    }
}
