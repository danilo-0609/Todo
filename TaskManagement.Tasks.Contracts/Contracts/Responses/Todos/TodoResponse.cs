using TodoManagement.Todos.Contracts.Contracts.Responses.Archives;
using TodoManagement.Todos.Contracts.Contracts.Responses.Comments;

namespace TodoManagement.Todos.Contracts.Contracts.Responses.Todos
{
    public record TodoResponse(string Id,
                               string Name,
                               string Description,
                               string? RecurringTodo,
                               CommentResponse? Comment,
                               List<string>? TodoTags,
                               List<ArchiveResponse>? Archives,
                               DateTime CreatedDateTime,
                               DateTime UpdatedDateTime);
}
