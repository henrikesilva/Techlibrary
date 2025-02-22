using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Security.Tokens.Access;

namespace TechLibrary.Infraestructure.Security.Tokens.Access;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly string _signingKey;
    public JwtTokenGenerator(string signingKey)
    {
        _signingKey = signingKey;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(60),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var symmetricKey = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(symmetricKey);
    }
}
