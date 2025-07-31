namespace Backend.Utility;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Backend.Models;

public static class JwtToken {
    private static readonly string Secret = "your-super-secret-key-change-this"; // store securely (env/secret store)
    private static readonly byte[] Key = Encoding.UTF8.GetBytes(Secret);

    public static string Generate(User user, TimeSpan? expiration = null) {
        var handler = new JwtSecurityTokenHandler();
        var descriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("username", user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? "")
            }),
            Expires = DateTime.UtcNow.Add(expiration ?? TimeSpan.FromDays(7)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }

    public static ClaimsPrincipal? Validate(string token) {
        var handler = new JwtSecurityTokenHandler();
        try {
            return handler.ValidateToken(token, new TokenValidationParameters {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromSeconds(5)
            }, out _);
        } catch {
            return null;
        }
    }

    public static Guid? GetUserId(string token) {
        var principal = Validate(token);
        var id = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(id, out var guid) ? guid : null;
    }
}