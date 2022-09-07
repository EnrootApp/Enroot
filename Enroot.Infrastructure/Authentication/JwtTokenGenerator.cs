using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Enroot.Application.Common.Interfaces.Authentication;
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

    public string GenerateToken(int userId, IList<Claim> claims)
    {
        var jwtSecret = _jwtSettings.Secret;
        var issuer = _jwtSettings.Issuer;
        var audience = _jwtSettings.Audience;
        var expiryHours = _jwtSettings.ExpiryHours;

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)), SecurityAlgorithms.HmacSha256);

        var jtiClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
        var userIdClaim = new Claim(JwtClaimNames.UserId, userId.ToString());

        claims.Add(jtiClaim);
        claims.Add(userIdClaim);

        var securityToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expiryHours));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}