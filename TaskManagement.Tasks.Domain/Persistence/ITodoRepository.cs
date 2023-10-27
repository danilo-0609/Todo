using ErrorOr;
using TodoManagement.Todos.Domain.Core.AggregateRoot;
using TodoManagement.Todos.Domain.Core.ValueObjects;

namespace TodoManagement.Todos.Domain.Persistence
{
    public interface ITodoRepository
    {
        //Implementar CRUD.

        Task<Todo?> GetTodoByIdAsync(TodoID taskId);

        Task<List<Todo?>> GetTodoByNameAsync(string Name);

        Task<List<Todo>> GetAllTodosAsync();

        Task DeleteTodoAsync(TodoID taskId);

        Task AddCommentAsync(TodoID taskId, CommentID taskCommentId);

        Task DeleteCommentAsync(CommentID taskCommentId);

        Task AddArchiveAsync(TodoID taskId, ArchiveID archiveId);

        Task DeleteArchiveAsync(TodoID taskId, ArchiveID archiveId);

        Task AddTodoAsync(ErrorOr<Todo> task);

        Task UpdateTodoAsync(ErrorOr<Todo> taskUpdated, TodoID taskId);
    }
}
