using TodoManagement.BuildingBlocks.Domain;

namespace TodoManagement.Users.Domain.UsersRegistration.Events;

public record UserRegistrationExpiredEvent(UserRegistrationId UserRegistrationId) 
                                                : IDomainEvent;
