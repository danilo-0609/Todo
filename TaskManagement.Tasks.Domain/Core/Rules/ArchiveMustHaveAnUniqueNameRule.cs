using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Domain.Core.Rules;

public sealed class ArchiveMustHaveAnUniqueNameRule : IBusinessRules
{
    private readonly IArchiveRepository _archiveRepository;
    private readonly string _name;

    public ArchiveMustHaveAnUniqueNameRule(IArchiveRepository archiveRepository, string name)
    {
        _archiveRepository = archiveRepository;
        _name = name;
    }

    public string Message => "The archive's name must be unique";

    public bool IsBroken()
    {
        return Evaluate().Result;

        async Task<bool> Evaluate()
        {
            var isUnique = await NameIsUnique();
        
        if (!isUnique)
            {
                return false;
            }

            return true;
        }
    }

    public async Task<bool> NameIsUnique()
    {
        if (await _archiveRepository.ArchiveNameIsUnique(_name))
        {
            return true;
        }

        return false;
    }
}
