using TodoManagement.BuildingBlocks.Domain;

namespace TodoManagement.Users.Domain.UsersRegistration.Rules;

public sealed class UserLoginMustBeUniqueRule : IBusinessRule
{
    private readonly IUsersCounter _usersCounter;
    private readonly string _login;

    internal UserLoginMustBeUniqueRule(IUsersCounter usersCounter, string login)
    {
        _usersCounter = usersCounter;
        _login = login;
    }


    public string Message => "User login must be unique";

    public bool IsBroken() => _usersCounter.CountUsersWithLogin(_login) > 0;
    
}
