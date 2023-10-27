using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Application.Entities.Queries.GetAll.Comments;

public sealed record GetAllCommentsQuery() : IRequest<ErrorOr<List<Comment>>>;

