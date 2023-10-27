using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Common.Errors;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Queries.GetAll.Archives;

internal sealed class GetAllArchivesQueryHandler : IRequestHandler<GetAllArchivesQuery, ErrorOr<List<Archive>>>
{
    private readonly IArchiveRepository _archiveRepository;

    public GetAllArchivesQueryHandler(IArchiveRepository archiveRepository)
    {
        _archiveRepository = archiveRepository ?? throw new ArgumentNullException(nameof(archiveRepository));
    }

    public async Task<ErrorOr<List<Archive>>> Handle(
        GetAllArchivesQuery request, 
        CancellationToken cancellationToken)
    {
        List<Archive> archives = await _archiveRepository.GetAllArchives();

        if (archives.Count == 0)
        {
            return Errors.Todos.NoArchivesYet;            
        }

        return archives;
    }
}
