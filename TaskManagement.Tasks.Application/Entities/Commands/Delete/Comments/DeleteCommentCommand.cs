using ErrorOr;
using MediatR;

namespace TodoManagement.Todos.Application.Entities.Commands.Delete.Comments;

public sealed record DeleteCommentCommand(Guid TodoId, Guid CommentId) : IRequest<ErrorOr<Unit>>;
