namespace TodoManagement.Users.Domain.UsersRegistration;

public interface IUsersCounter
{
    int CountUsersWithLogin(string login);
}
