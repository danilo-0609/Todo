using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.AggregateRoot;

namespace TodoManagement.Todos.Domain.Core.Events
{
    public record TodoCompletedEvent(TodoCompleted TaskCompleted) : IDomainEvent;
}
