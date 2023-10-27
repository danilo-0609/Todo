using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Core.ValueObjects;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Queries.GetById.Archives;

internal sealed class GetArchiveByIdQueryHandler : IRequestHandler<GetArchiveByIdQuery, ErrorOr<Archive>>
{
    private readonly IArchiveRepository _archiveRepository;

    internal GetArchiveByIdQueryHandler(IArchiveRepository archiveRepository)
    {
        _archiveRepository = archiveRepository;
    }

    public async Task<ErrorOr<Archive>> Handle(GetArchiveByIdQuery query,
        CancellationToken cancellationToken)
    {
        var archiveID = ArchiveID.Create(query.Id);

        if (await _archiveRepository.GetArchiveByIdAsync(archiveID) is not Archive archive)
        {
            return Error.NotFound("Archive.NotFound", "The archive was not found");
        }

        return archive;
    }
}
