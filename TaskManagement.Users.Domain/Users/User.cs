using ErrorOr;
using TodoManagement.BuildingBlocks.Domain;
using TodoManagement.Users.Domain.Common.ValueObjects;
using TodoManagement.Users.Domain.Users.Events;
using TodoManagement.Users.Domain.Users.ValueObjects;

namespace TodoManagement.Users.Domain.Users;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public new UserId Id { get; private set; }

    public string Login { get; private set; }

    public Password Password { get; private set; }

    public Email Email { get; private set; }

    public bool IsActive { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    private User()
    {
        //EF
    }

    public static ErrorOr<User> Create(Guid id, string login, string password, 
        string emailValue, string firstName, string lastName)
    {
        var passwordHash = Password.Create(password);

        if (passwordHash.IsError)
        {
            return passwordHash.FirstError;
        }

        var email = Email.Create(emailValue);

        if (email.IsError)
        {
            return email.FirstError;
        }

        var user = new User(id, login, passwordHash.Value, email.Value, firstName, lastName);

        return user;
    }

    private User(
        Guid id,
        string login, 
        Password password,
        Email email,
        string firstName,
        string lastName)
    {
         Id = UserId.Create(id);
        Login = login;
        Password = password;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Name = $"{firstName} {lastName}";

        IsActive = true;
        CreatedDateTime = DateTime.UtcNow;

        AddDomainEvent(new UserCreatedEvent(Id));
    }
}
