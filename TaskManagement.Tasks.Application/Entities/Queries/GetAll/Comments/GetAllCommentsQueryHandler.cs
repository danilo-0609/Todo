using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Common.Errors;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Queries.GetAll.Comments;

internal sealed class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, ErrorOr<List<Comment>>>
{
    private readonly ICommentRepository _commentRepository;

    internal GetAllCommentsQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<ErrorOr<List<Comment>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        List<Comment> comments = await _commentRepository.GetAllComments();

        if (comments.Count == 0)
        {
            return Errors.Todos.NoCommentsYet;
        }

        return comments;
    }
}
