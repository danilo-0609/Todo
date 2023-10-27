using ErrorOr;
using MediatR;
using TodoManagement.Users.Application.Abstractions;
using TodoManagement.Users.Contracts.Results;
using TodoManagement.Users.Domain.Common;
using TodoManagement.Users.Domain.Common.DomainErrors;
using TodoManagement.Users.Domain.Users;
using TodoManagement.Users.Domain.Users.ValueObjects;

namespace TodoManagement.Users.Application.Authentication.RegisterUsers.Command;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    internal RegisterUserCommandHandler(IJwtGenerator jwtGenerator, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterUserCommand command, 
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (await _userRepository.GetByEmailAsync(command.Email) is not null)
        {
            return Errors.Users.DuplicatedEmail;
        }

        var password = Password.Create(command.Password);

        if (password.IsError)
        {
            return password.FirstError;
        }


        var user = User.Create(Guid.NewGuid(), 
                                command.Login, 
                                password.Value.PasswordValue, 
                                command.Email, 
                                command.FirstName, 
                                command.LastName);

        await _userRepository.AddAsync(user.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        string token = _jwtGenerator.Generate(user.Value);

        var authenticationResult = new AuthenticationResult(command.Login, token);

        return authenticationResult;
    }
}