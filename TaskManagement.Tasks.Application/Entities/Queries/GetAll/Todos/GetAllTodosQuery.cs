using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.AggregateRoot;

namespace TodoManagement.Todos.Application.Entities.Queries.GetAll.Todos;

public sealed record GetAllTodosQuery() : IRequest<ErrorOr<List<Todo>>>;
