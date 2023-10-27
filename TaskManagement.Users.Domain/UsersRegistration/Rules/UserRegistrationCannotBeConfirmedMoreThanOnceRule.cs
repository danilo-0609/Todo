using TodoManagement.BuildingBlocks.Domain;
using TodoManagement.Users.Domain.UsersRegistration.ValueObjects;

namespace TodoManagement.Users.Domain.UsersRegistration.Rules;

public sealed class UserRegistrationCannotBeConfirmedMoreThanOnceRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeConfirmedMoreThanOnceRule(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public string Message => "User registration cannot be confirmed more than once";

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Confirmed;
}
