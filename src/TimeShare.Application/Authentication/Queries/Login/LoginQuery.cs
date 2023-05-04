using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Authentication.Common;

namespace TimeShare.Application.Authentication.Queries.Login;

public sealed record LoginQuery(
    string Email,
    string Password) : IQuery<AuthenticationResult>;