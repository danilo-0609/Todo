using ErrorOr;
using MediatR;
using TodoManagement.Users.Contracts.Results;

namespace TodoManagement.Users.Application.Authentication.LoginUsers.Query;

public sealed record LoginUserQuery(string Email, string Password) 
            : IRequest<ErrorOr<AuthenticationResult>>;
