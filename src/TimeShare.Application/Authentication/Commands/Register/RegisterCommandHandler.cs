using ErrorOr;
using TimeShare.Application.Abstractions.Authentication;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Application.Authentication.Common;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Authentication.Commands.Register;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (!await _userRepository.EmailIsUniqueAsync(command.Email, cancellationToken))
        {
            return DomainErrors.User.DuplicateEmail;
        }

        var user = User.Create(command.FirstName, command.LastName, command.Email, command.Password);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        string token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}