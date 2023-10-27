using ErrorOr;
using TodoManagement.Todos.Domain.Common.Errors;
using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.Events;
using TodoManagement.Todos.Domain.Core.Rules;
using TodoManagement.Todos.Domain.Core.ValueObjects;

namespace TodoManagement.Todos.Domain.Core.Entities
{
    public class Comment : Entity<CommentID>
    {
        public string Value { get; private set; }

        private Comment(CommentID taskCommentId, string comment)
            : base(taskCommentId)
        {
            Value = comment;
        }


        public static ErrorOr<Comment> CreateComment(string commentContent)
        {
            var comment = new Comment(CommentID.Create(Guid.NewGuid()), commentContent);

            CommentLengthRule commentLengthRule = new(comment);

            if (commentLengthRule.IsBroken())
            {
                return Errors.Tasks.CommentLengthGreaterThanRequired(commentLengthRule);
            }

            comment.AddDomainEvent(new CommentCreatedEvent(comment));

            return comment;
        }

        public static ErrorOr<Comment> UpdateComment(CommentID id, string newComment)
        {
            var comment = new Comment(id, newComment);

            CommentLengthRule commentLengthRule = new(comment);

            if (commentLengthRule.IsBroken())
            {
                return Errors.Tasks.CommentLengthGreaterThanRequired(commentLengthRule);
            }

            comment.AddDomainEvent(new CommentUpdatedEvent(comment));

            return comment;
        }




    }
}
