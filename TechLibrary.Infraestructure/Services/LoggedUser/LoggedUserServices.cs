using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Security.Tokens;
using TechLibrary.Domain.Services.LoggedUser;
using TechLibrary.Infraestructure.DataAccess;

namespace TechLibrary.Infraestructure.Services.LoggedUser;
public class LoggedUserServices : ILoggedUserServices
{
    private readonly TechLibraryDbContext _context;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUserServices(TechLibraryDbContext context,
        ITokenProvider tokenProvider)
    {
        _context = context;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

        var userId = Guid.Parse(identifier);

        return await _context.Users
                    .AsNoTracking()
                    .FirstAsync(user => user.Id == userId);
    }
}
