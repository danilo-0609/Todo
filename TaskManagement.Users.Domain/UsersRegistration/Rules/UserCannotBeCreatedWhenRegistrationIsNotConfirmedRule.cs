using TodoManagement.BuildingBlocks.Domain;
using TodoManagement.Users.Domain.UsersRegistration.ValueObjects;

namespace TodoManagement.Users.Domain.UsersRegistration.Rules;

public sealed class UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public string Message => "User cannot be created when registration is not confirmed";

    public bool IsBroken() => _actualRegistrationStatus != UserRegistrationStatus.Confirmed;
}
