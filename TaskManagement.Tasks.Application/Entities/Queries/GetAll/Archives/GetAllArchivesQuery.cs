using ErrorOr;
using MediatR;
using TodoManagement.Todos.Domain.Core.Entities;

namespace TodoManagement.Todos.Application.Entities.Queries.GetAll.Archives;

public sealed record GetAllArchivesQuery() : IRequest<ErrorOr<List<Archive>>>;
