// namespace JWT;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Intacct.Models;
using System.Net.Mail;

public class JWT
{
    public string Secret;
    public string Issuer;
    public string Audience;
    public JWT()
    {
         var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
         var section = config.GetSection("JWT");
         Secret = section["Secret"]!;
         Issuer = section["ValidIssuer"]!;
         Audience = section["ValidAudience"]!;
    }
    public string getToken(User user){
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                new Claim(ClaimTypes.Email, user.Email!),
                
            };
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Secret));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
                    issuer: this.Issuer,
                    audience: this.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );
        return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    }
}