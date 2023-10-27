using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Commands.Create.Comments;

internal class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ErrorOr<Comment>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ITodoRepository todoRepository,
          IUnitOfWork unitOfWork, ICommentRepository commentRepository)
    {
        _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<ErrorOr<Comment>> Handle(
        CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        var todoID = TodoID.Create(command.TodoId);
        var comment = Comment.CreateComment(command.Comment);

        if (comment.IsError)
        {
            return comment.FirstError;
        }

        await _todoRepository.AddCommentAsync(todoID, comment.Value.Id);
        await _commentRepository.AddComment(todoID, comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return comment;
    }
}
