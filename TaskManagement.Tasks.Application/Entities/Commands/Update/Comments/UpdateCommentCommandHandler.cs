using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Commands.Update.Comments;

internal sealed class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, ErrorOr<Comment>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCommentCommandHandler(ICommentRepository commentRepository,
        IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Comment>>
            Handle(UpdateCommentCommand request,
            CancellationToken cancellationToken)
    {
        var commentId = CommentID.Create(request.CommentId);
        var todoId = TodoID.Create(request.TodoId);

        var currentTaskComment = await _commentRepository.GetCommentById(commentId);

        if (currentTaskComment is null)
        {
            return Error.NotFound("Comment.NotFound", "The comment with the ID provided was not found");
        }

        var commentUpdate =
                Comment.UpdateComment(commentId, request.Comment);

        if (commentUpdate.IsError)
        {
            return commentUpdate.FirstError;
        }

        //_emailService.Send();
        await _commentRepository.UpdateComment(todoId, commentUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return commentUpdate;
    }
}
