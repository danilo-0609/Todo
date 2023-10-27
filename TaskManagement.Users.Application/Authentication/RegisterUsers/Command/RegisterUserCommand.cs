using ErrorOr;
using MediatR;
using TodoManagement.Users.Contracts.Results;

namespace TodoManagement.Users.Application.Authentication.RegisterUsers.Command;

public sealed record RegisterUserCommand(string Login, string FirstName, 
                                         string LastName, string Email, 
                                         string Password) : IRequest<ErrorOr<AuthenticationResult>>; 
