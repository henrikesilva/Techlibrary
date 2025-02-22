namespace TechLibrary.Domain.Security.Cryptography;
public interface IBCryptAlgorithm
{
    public string HashPassword(string password);
    public bool Verify(string password, Entities.User user);
}
