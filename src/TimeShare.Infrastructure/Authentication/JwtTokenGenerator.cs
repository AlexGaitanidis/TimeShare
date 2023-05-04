using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TimeShare.Application.Abstractions.Authentication;
using TimeShare.Application.Abstractions.Services;
using TimeShare.Domain.Aggregates.UserAggregate;

namespace TimeShare.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtOptions _jwtOptions;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtOptions> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new (JwtRegisteredClaimNames.GivenName, user.FirstName),
            new (JwtRegisteredClaimNames.FamilyName, user.LastName),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}