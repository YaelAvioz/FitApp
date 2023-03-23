using FitAppServer.DTO;
using FitAppServer.Model;
using System.Security.Cryptography;
using System.Text;

namespace FitAppServer.Services
{
    public class AccountService
    {
        public async Task<User> RegisterUser(RegisterDTO userDTO)
        {
            var isUserExist = await UserExists(userDTO.Username);
            if (isUserExist != null)
            {
                throw new Exception("User exists!");
            }
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Username = userDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                PasswordSalt = hmac.Key
            };
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(user);
                    await transaction.CommitAsync();
                }
            }
            return user;
        }

        public async Task<User> Login(LoginDTO userDTO)
        {
            var userExists = await UserExists(userDTO.Username);
            if (userExists == null) throw new Exception("Unauthorized user!");
            using var hmac = new HMACSHA512(userExists.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != userExists.PasswordHash[i]) throw new Exception("Unauthorized user!");
            }
            return userExists;
        }

        private async Task<User> UserExists(string Username)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var user = await session.Query<User>().Where(user => user.Username.ToLower() == Username.ToLower()).FirstOrDefaultAsync();
                return user;
            }
        }
    }
}

