using ErrorOr;
using MediatR;
using TodoManagement.BuildingBlocks.Domain;
using TodoManagement.Users.Domain.Common.DomainErrors;
using TodoManagement.Users.Domain.Common.ValueObjects;
using TodoManagement.Users.Domain.Users;
using TodoManagement.Users.Domain.UsersRegistration.Events;
using TodoManagement.Users.Domain.UsersRegistration.Rules;
using TodoManagement.Users.Domain.UsersRegistration.ValueObjects;

namespace TodoManagement.Users.Domain.UsersRegistration;

public sealed class UserRegistration : AggregateRoot<UserRegistrationId, Guid>
{
    public UserRegistrationId Id { get; private set; }

    private string _login;

    private string _password;

    private Email _email;

    private string _firstName;

    private string _lastName;

    private string _name;

    private DateTime _registerDate;

    private UserRegistrationStatus _status;

    private DateTime? _confirmedDate;

    private UserRegistration()
    {
        //Only EF.
    }

    public static ErrorOr<UserRegistration> RegisterNewUser(
        string login, 
        string password,
        string emailAddress,
        string firstName,
        string lastName,
        IUsersCounter usersCounter,
        string confirmLink)
    {
        UserLoginMustBeUniqueRule userLoginMustBeUniqueRule = new(usersCounter, login);

        if (userLoginMustBeUniqueRule.IsBroken())
        {
            return Errors.Users.UserLoginIsNotUnique(userLoginMustBeUniqueRule.Message);
        }

        var email = Email.Create(emailAddress);

        if (email.IsError)
        {
            return email.FirstError;
        }

        var userRegistration = new UserRegistration(login, password, email.Value, firstName, lastName);

        userRegistration.AddDomainEvent(new NewUserRegisteredEvent(
            userRegistration.Id,
            userRegistration._login,
            userRegistration._email.Value,
            userRegistration._firstName,
            userRegistration._lastName,
            userRegistration._name,
            userRegistration._registerDate,
            confirmLink));

        return userRegistration;
    }

    private UserRegistration(string login, string password, Email email, 
        string firstName, string lastName)
    {
         Id = UserRegistrationId.CreateUnique();
        _login = login;
        _password = password;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _name = $"{firstName} {lastName}";
        _registerDate = DateTime.UtcNow;
        _status = UserRegistrationStatus.WaitingForConfirmation;
    }


    public ErrorOr<Unit> Confirm()
    {
        UserRegistrationCannotBeConfirmedMoreThanOnceRule moreThanOnceRegistrationRule = new(_status);

        if (moreThanOnceRegistrationRule.IsBroken())
        {
            return Errors.Users.UserRegistrationConfirmedTwice(moreThanOnceRegistrationRule.Message);
        }

        UserRegistrationCannotBeConfirmedAfterExpirationRule cannotConfirmedAfterExpirationRule = new(_status);
    
        if (cannotConfirmedAfterExpirationRule.IsBroken())
        {
            return Errors.Users.UserRegistrationConfirmedAfterExpired(cannotConfirmedAfterExpirationRule.Message);
        }

        _status = UserRegistrationStatus.Confirmed;
        _confirmedDate = DateTime.UtcNow;

        AddDomainEvent(new UserRegistrationConfirmedEvent(Id));

        return Unit.Value;
    }

    public ErrorOr<Unit> Expire()
    {
        UserRegistrationCannotBeExpiredMoreThanOnce cannotBeExpiredMoreThanOnceRule = new(_status);

        if (cannotBeExpiredMoreThanOnceRule.IsBroken())
        {
            return Errors.Users.UserRegistrationExpiredTwice(cannotBeExpiredMoreThanOnceRule.Message);
        }

        _status = UserRegistrationStatus.Expired;

        AddDomainEvent(new UserRegistrationExpiredEvent(Id));

        return Unit.Value;
    }
}