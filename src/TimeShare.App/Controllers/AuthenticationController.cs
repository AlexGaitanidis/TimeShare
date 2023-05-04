using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeShare.Application.Authentication.Commands.Register;
using TimeShare.Application.Authentication.Queries.Login;
using TimeShare.Contracts.Authentication;
using TimeShare.Domain.Errors;

namespace TimeShare.App.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender sender, IMapper mapper) 
        : base(sender, mapper)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<RegisterCommand>(request);

        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            authenticationResult => Ok(Mapper.Map<AuthenticationResponse>(authenticationResult)),
            Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var query = Mapper.Map<LoginQuery>(request);

        var result = await Sender.Send(query, cancellationToken);

        if (result.IsError && result.FirstError == DomainErrors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: result.FirstError.Description);
        }

        return result.Match(
            authenticationResult => Ok(Mapper.Map<AuthenticationResponse>(authenticationResult)),
            Problem);
    }
}