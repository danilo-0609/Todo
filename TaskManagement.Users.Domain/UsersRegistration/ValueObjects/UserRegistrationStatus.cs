using TodoManagement.BuildingBlocks.Domain;

namespace TodoManagement.Users.Domain.UsersRegistration.ValueObjects;

public sealed class UserRegistrationStatus : ValueObject
{
    public static UserRegistrationStatus WaitingForConfirmation =>
                    new UserRegistrationStatus(nameof(WaitingForConfirmation));

    public static UserRegistrationStatus Confirmed =>
                    new UserRegistrationStatus(nameof(Confirmed));

    public static UserRegistrationStatus Expired =>
                    new UserRegistrationStatus(nameof(Expired));


    public string Value { get; }

    private UserRegistrationStatus(string value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
