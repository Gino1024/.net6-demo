using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Infrastructrue.Implement;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Infrastructrue.Interface
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenProvider(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GenerateToken(object instance = null)
        {
            string userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].FirstOrDefault();
            string userAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var authClaims = new List<Claim>
            {
                 new Claim("userAgent",userAgent),
                 new Claim("userAddress",userAddress),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            if (instance != null)
            {
                var props = instance.GetType().GetProperties();
                foreach (var prop in props)
                {
                    authClaims.Add(new Claim(prop.Name, prop.GetValue(instance)?.ToString()));
                }

            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Issuer"],
                            
                            claims: authClaims,
                            expires: DateTime.Now.AddHours(Convert.ToInt32(_configuration["Jwt:Expire"])),
                            signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                            );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }

        public string ValidateToken()
        {
            throw new NotImplementedException();
        }
    }
}
