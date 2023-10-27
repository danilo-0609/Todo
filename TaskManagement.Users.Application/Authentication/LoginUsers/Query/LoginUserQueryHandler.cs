using ErrorOr;
using MediatR;
using TodoManagement.Users.Application.Abstractions;
using TodoManagement.Users.Contracts.Results;
using TodoManagement.Users.Domain.Common.DomainErrors;
using TodoManagement.Users.Domain.Users;

namespace TodoManagement.Users.Application.Authentication.LoginUsers.Query;

internal sealed class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginUserQueryHandler(IUserRepository userRepository, IJwtGenerator jwtGenerator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginUserQuery query, 
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (await _userRepository.GetByEmailAsync(query.Email) is not User user)
        {
            return Errors.Users.UserNotFound;
        }

        if (query.Password != user.Password)
        {
            return Errors.Users.InvalidPassword;
        }

        var token = _jwtGenerator.Generate(user);
        var authenticationResult = new AuthenticationResult(user.Login, token);

        return authenticationResult;
    }
}
