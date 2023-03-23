using FitAppServer.Interfaces;
using FitAppServer.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitAppServer.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])
            );
        }
        public string CreateToken(User user)
        {
            // Represent the claims or information to be encoded in the token.
            var claims = new List<Claim>{
            new Claim( JwtRegisteredClaimNames.NameId,user.Username)
            };
            // class, which is used to sign the token
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

            // class, which is used to describe the token.
            // This class store information on the token.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // property is to provide information about the token's owner
                Subject = new ClaimsIdentity(claims),
                // Date and time when the token expires
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            //designed for creating and validating Json Web Tokens
            var tokenHandler = new JwtSecurityTokenHandler();
            // Create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}