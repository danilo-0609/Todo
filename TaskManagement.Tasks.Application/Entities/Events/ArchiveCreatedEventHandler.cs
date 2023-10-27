using MediatR;
using TodoManagement.Todos.Domain.Core.Events;

namespace TodoManagement.Todos.Application.Entities.Events
{
    public class ArchiveCreatedEventHandler : INotificationHandler<ArchiveCreatedEvent>
    {
        public Task Handle(ArchiveCreatedEvent notification, CancellationToken cancellationToken)
        {
            //_emailService.Send();

            return Task.CompletedTask;
        }
    }
}
