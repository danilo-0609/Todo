using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Commands.Delete.Comments;

internal sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, ErrorOr<Unit>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;

    internal DeleteCommentCommandHandler(ICommentRepository commentRepository,
        ITodoRepository todoRepository,
        IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    }

    public async Task<ErrorOr<Unit>> Handle(
        DeleteCommentCommand command,
        CancellationToken cancellationToken)
    {
        var commentId = CommentID.Create(command.CommentId);
        var todoId = TodoID.Create(command.TodoId);

        if (await _commentRepository.GetCommentById(commentId) is null)
        {
            return Error.NotFound("Task.Comment", "The comment with the id provided was not found");
        }

        await _commentRepository.DeleteComment(todoId, commentId);
        await _todoRepository.DeleteCommentAsync(commentId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
