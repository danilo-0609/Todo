using MediatR;
using TodoManagement.Users.Domain.Users.Events;

namespace TodoManagement.Users.Application.Events;

public sealed class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        throw new NotImplementedException();

        //emailService.Send();
    }
}
