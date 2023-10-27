using TodoManagement.BuildingBlocks.Domain;

namespace TodoManagement.Users.Domain.Users.Events;

public sealed class UserCreatedEvent : IDomainEvent
{
    public UserCreatedEvent(UserId id)
    {
        Id = id;
    }

    public UserId Id { get; }
}