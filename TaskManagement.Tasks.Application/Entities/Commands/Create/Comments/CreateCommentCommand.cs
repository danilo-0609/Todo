using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Application.Entities.Commands.Create.Comments
{
    public record CreateCommentCommand(Guid TodoId, string Comment) : IRequest<ErrorOr<Comment>>;
}
