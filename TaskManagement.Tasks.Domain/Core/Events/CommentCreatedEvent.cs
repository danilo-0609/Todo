using TodoManagement.Todos.Domain.Common.Primitives;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Domain.Core.Events
{
    public record CommentCreatedEvent(Comment Comment) : IDomainEvent;
}
