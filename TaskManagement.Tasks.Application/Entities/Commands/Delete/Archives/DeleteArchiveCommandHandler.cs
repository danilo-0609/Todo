using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Commands.Delete.Archives;

internal sealed class DeleteArchiveCommandHandler : IRequestHandler<DeleteArchiveCommand, ErrorOr<Unit>>
{
    private readonly IArchiveRepository _archiveRepository;
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;

    internal DeleteArchiveCommandHandler(IArchiveRepository archiveRepository,
        ITodoRepository todoRepository,
        IUnitOfWork unitOfWork)
    {
        _archiveRepository = archiveRepository ?? throw new ArgumentNullException(nameof(archiveRepository));
        _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(
        DeleteArchiveCommand command,
        CancellationToken cancellationToken)
    {
        var archiveID = ArchiveID.Create(command.ArchiveID);
        var todoID = TodoID.Create(command.TodoId);

        var currentArchive = await _archiveRepository.GetArchiveByIdAsync(archiveID);

        if (currentArchive is null)
        {
            return Error.NotFound("Archive.NotFound", "The archive was not found");
        }

        //_emailService.Send();
        await _archiveRepository.DeleteArchiveAsync(archiveId: archiveID);
        await _todoRepository.DeleteArchiveAsync(todoID, archiveID);
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return Unit.Value;
    }
}
