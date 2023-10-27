using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Core.Rules;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Commands.Create.Archives
{
    internal sealed class CreateArchiveCommandHandler : IRequestHandler<CreateArchiveCommand, ErrorOr<Archive>>
    {
        private readonly IArchiveRepository _archiveRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITodoRepository _todoRepository;


        internal CreateArchiveCommandHandler(IArchiveRepository archiveRepository,
            IUnitOfWork unitOfWork, ITodoRepository todoRepository)
        {
            _archiveRepository = archiveRepository ?? throw new ArgumentNullException(nameof(archiveRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        public async Task<ErrorOr<Archive>> Handle(
            CreateArchiveCommand command,
            CancellationToken cancellationToken)
        {
            var todoID = TodoID.Create(command.TodoId);

            var archive = Archive.Create(command.ArchivePDF, command.Name);

            if (archive.IsError)
            {
                return archive.FirstError;
            }

            //Guardar en disco luego
            await _archiveRepository.AddArchiveAsync(command.ArchivePDF);
            await _todoRepository.AddArchiveAsync(todoID, archive.Value.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return archive;
        }
    }
}
