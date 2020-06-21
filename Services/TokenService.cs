using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NorusContract.Domain.Entities;

namespace NorusContract.Services
{
  public interface ITokenService
  {
    string GenerateToken(User user);
  }

  public class TokenService : ITokenService
  {
    public string GenerateToken(User user)
    {
      var tokenHandle = new JwtSecurityTokenHandler();
      
      var key = Encoding.ASCII.GetBytes("senha_super_secreta");

      var _subject = new ClaimsIdentity(new Claim[]
      {
        new Claim("UserId", user.UserId.ToString()),
        new Claim("UserName", user.UserName)
      });

      var tokenDescription = new SecurityTokenDescriptor
      {
        Subject = _subject,
        Expires = DateTime.UtcNow.AddHours(6),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandle.CreateToken(tokenDescription);

      return tokenHandle.WriteToken(token);
    }
  }
}