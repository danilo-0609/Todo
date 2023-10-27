using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Application.Entities.Commands.Update.Comments;

public record UpdateCommentCommand(Guid TodoId, Guid CommentId, string Comment) : IRequest<ErrorOr<Comment>>;
