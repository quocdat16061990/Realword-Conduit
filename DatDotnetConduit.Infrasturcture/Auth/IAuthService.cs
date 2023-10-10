using DatDotnetConduit.Domain.Entities;
namespace DatDotnetConduit.Infrasturcture.Auth
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
        public string HashPassword(string password);
        public bool VerifyPassword(string hashPassword, string plainPassword);
    }
}
