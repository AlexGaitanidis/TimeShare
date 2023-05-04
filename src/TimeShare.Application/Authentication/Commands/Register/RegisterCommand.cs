using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Authentication.Common;

namespace TimeShare.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<AuthenticationResult>;