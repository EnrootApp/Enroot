using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Enroot.Application.Common.Interfaces.Authentication;
using Enroot.Domain.User;
using Enroot.Domain.User.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Enroot.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        var jwtSecret = _jwtSettings.Secret;
        var issuer = _jwtSettings.Issuer;
        var audience = _jwtSettings.Audience;
        var expiryHours = _jwtSettings.ExpiryHours;

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();

        var jtiClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
        var userIdClaim = new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString()!);

        var roleClaim = new Claim(ClaimTypes.Role, user.Role);

        claims.Add(jtiClaim);
        claims.Add(userIdClaim);
        claims.Add(roleClaim);

        var securityToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(expiryHours),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}