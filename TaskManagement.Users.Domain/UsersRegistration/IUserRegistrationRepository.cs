namespace TodoManagement.Users.Domain.UsersRegistration;

public interface IUserRegistrationRepository
{
    Task AddAsync(UserRegistration userRegistration);

    Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId);
}
