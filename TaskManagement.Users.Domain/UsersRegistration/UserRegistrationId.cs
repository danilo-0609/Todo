using TodoManagement.BuildingBlocks.Domain;

namespace TodoManagement.Users.Domain.UsersRegistration;

public sealed class UserRegistrationId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private UserRegistrationId(Guid value)
    {
        Value = value;
    }

    public static UserRegistrationId CreateUnique() => new UserRegistrationId(Guid.NewGuid());

    public static UserRegistrationId Create(Guid value) => new UserRegistrationId(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private UserRegistrationId() { }
}
