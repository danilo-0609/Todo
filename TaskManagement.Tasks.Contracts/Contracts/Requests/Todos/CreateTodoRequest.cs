namespace TodoManagement.Todos.Contracts.Contracts.Requests.Todos;

public record CreateTodoRequest(string Name, string Description, string? RecurringTodo,
                                List<string>? Tag);
