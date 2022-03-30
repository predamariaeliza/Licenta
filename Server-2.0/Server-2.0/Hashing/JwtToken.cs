using Microsoft.IdentityModel.Tokens;
using Server_2._0.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server_2._0.Hashing
{
    public class JwtToken : IJwtToken
    {
        private readonly IConfiguration _configuration;
        public JwtToken(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public String CreateToken(UserModel User)
        {
            //list of claims in which we store username, email and role
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                new Claim(ClaimTypes.Name, User.Username),
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.Role,User.Role)
            };

            //creeaza o cheie bazata pe un string
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );
            //encoding  using sha512 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //creating a tokendescriptor based on claims and an expiry date
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //claims
                Subject = new ClaimsIdentity(claims),
                //cand va expira
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = credentials
            };

            //token handler
            //token handler + token descriptor = security token
            var tokenHandler = new JwtSecurityTokenHandler();

            //OUR SECURITY TOKEN
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public JwtSecurityToken VerifyToken(String Token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["jwt_secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
}
