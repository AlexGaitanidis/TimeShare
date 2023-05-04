using ErrorOr;
using TimeShare.Application.Abstractions.Authentication;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Application.Authentication.Common;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Authentication.Queries.Login;

internal sealed class LoginQueryHandler : IQueryHandler<LoginQuery, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(query.Email, cancellationToken);

        if (user is null || user.Password != query.Password)
        {
            return DomainErrors.Authentication.InvalidCredentials;
        }

        string token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}