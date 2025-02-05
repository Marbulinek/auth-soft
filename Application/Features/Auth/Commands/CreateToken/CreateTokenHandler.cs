using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Auth.Commands.CreateToken;

public class CreateTokenHandler(IConfiguration configuration) : IRequestHandler<CreateTokenCommand, string>
{
    public Task<string> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, request.User.Username),
            new(ClaimTypes.NameIdentifier, request.User.Id.ToString()),
            new(ClaimTypes.Role, request.User.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: creds                
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokenDescriptor));
    }
}