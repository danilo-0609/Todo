using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Common.Errors;
using TodoManagement.Todos.Domain.Core.AggregateRoot;
using TodoManagement.Todos.Domain.Persistence;

namespace TodoManagement.Todos.Application.Entities.Queries.GetAll.Todos;

internal sealed class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, ErrorOr<List<Todo>>>
{
    private readonly ITodoRepository _todoRepository;

    public GetAllTodosQueryHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
    }

    public async Task<ErrorOr<List<Todo>>> Handle(GetAllTodosQuery request, 
        CancellationToken cancellationToken)
    {
        List<Todo> todos = await _todoRepository.GetAllTodosAsync();

        if (todos.Count == 0)
        {
            return Errors.Todos.NoTodosYet;
        }

        return todos;
    }
}
