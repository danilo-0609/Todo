using TodoManagement.BuildingBlocks.Domain;
using TodoManagement.Users.Domain.UsersRegistration.ValueObjects;

namespace TodoManagement.Users.Domain.UsersRegistration.Rules;

public sealed class UserRegistrationCannotBeConfirmedAfterExpirationRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeConfirmedAfterExpirationRule(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public string Message => "User registration cannot be confirmed after expiration";

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;
}
