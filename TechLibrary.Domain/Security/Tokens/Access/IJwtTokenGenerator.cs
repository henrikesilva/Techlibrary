namespace TechLibrary.Domain.Security.Tokens.Access;
public interface IJwtTokenGenerator
{
    string GenerateToken(Entities.User user);
}
