using TodoManagement.BuildingBlocks.Domain;

namespace TodoManagement.Users.Domain.UsersRegistration.Events;

public record UserRegistrationConfirmedEvent(UserRegistrationId UserRegistrationId) 
                                                : IDomainEvent;
