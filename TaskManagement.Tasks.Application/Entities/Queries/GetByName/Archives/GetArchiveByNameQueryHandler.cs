using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Queries.GetByName.Archives;

internal sealed class GetArchiveByNameQueryHandler : IRequestHandler<GetArchiveByNameQuery, ErrorOr<Archive>>
{
    private readonly IArchiveRepository _archiveRepository;

    internal GetArchiveByNameQueryHandler(IArchiveRepository archiveRepository)
    {
        _archiveRepository = archiveRepository;
    }

    public async Task<ErrorOr<Archive>> Handle(GetArchiveByNameQuery query,
        CancellationToken cancellationToken)
    {
        var name = query.Name;

        if (await _archiveRepository.GetArchivesByName(name) is not Archive archive)
        {
            return Error.NotFound("Archive.NotFound", $"The archive with the name {name} was not found");
        }

        return archive!;
    }
}
