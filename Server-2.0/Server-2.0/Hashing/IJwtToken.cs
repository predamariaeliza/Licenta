using Server_2._0.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Server_2._0.Hashing
{
    public interface IJwtToken
    {
        string CreateToken(UserModel User);
        JwtSecurityToken VerifyToken(string Token);
    }
}
