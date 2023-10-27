using ErrorOr;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Core.ValueObjects;

namespace TodoManagement.Todos.Domain.Persistence;

public interface ICommentRepository
{
    Task AddComment(TodoID taskId, ErrorOr<Comment> taskComment);

    Task UpdateComment(TodoID taskId, ErrorOr<Comment> taskComment);

    Task<Comment?> GetCommentById(CommentID taskCommentId);

    Task<List<Comment>> GetAllComments();

    Task DeleteComment(TodoID taskId, CommentID taskCommentId);
}
