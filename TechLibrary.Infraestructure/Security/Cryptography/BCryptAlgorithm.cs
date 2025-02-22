using TechLibrary.Domain.Security.Cryptography;

namespace TechLibrary.Infraestructure.Security.Cryptography;
public class BCryptAlgorithm : IBCryptAlgorithm
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, Domain.Entities.User user) => BCrypt.Net.BCrypt.Verify(password, user.Password);
}
