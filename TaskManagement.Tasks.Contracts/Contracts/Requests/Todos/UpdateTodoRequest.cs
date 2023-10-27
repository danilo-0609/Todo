namespace TodoManagement.Todos.Contracts.Contracts.Requests.Todos;

public sealed record UpdateTodoRequest(Guid Id, string Name, string Description,
                                       string? RecurringTodo = null, List<string>? Tags = null);
