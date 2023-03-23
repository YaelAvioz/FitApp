using FitAppServer.Model;

namespace FitAppServer.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}