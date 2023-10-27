using TodoManagement.BuildingBlocks.Domain;
using TodoManagement.Users.Domain.UsersRegistration.ValueObjects;

namespace TodoManagement.Users.Domain.UsersRegistration.Rules;

public sealed class UserRegistrationCannotBeExpiredMoreThanOnce : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    public UserRegistrationCannotBeExpiredMoreThanOnce(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public string Message => "User registration cannot be expired more than once";

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;
}
