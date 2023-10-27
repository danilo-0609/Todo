using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Commands.Delete.Todos;

internal sealed class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, ErrorOr<Unit>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;

    internal DeleteTodoCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork)
    {
        _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    }

    public async Task<ErrorOr<Unit>> Handle(
        DeleteTodoCommand command,
        CancellationToken cancellationToken)
    {

        var todoId = TodoID.Create(command.TodoId);

        if (await _todoRepository.GetTodoByIdAsync(todoId) is null)
        {
            return Error.NotFound("Todo.NotFound", "The todo was not found");
        }

        //_emailService.Send();
        await _todoRepository.DeleteTodoAsync(todoId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
