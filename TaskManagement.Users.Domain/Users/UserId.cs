using TodoManagement.BuildingBlocks.Domain;

namespace TodoManagement.Users.Domain.Users;

public sealed class UserId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique() => new UserId(Guid.NewGuid());

    public static UserId Create(Guid value) => new UserId(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private UserId() { }
}
