using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tour.Application.Common.Interfaces.Authentication;
using Tour.Application.Common.Services;

namespace Tour.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IConfiguration configuration)
        {
            _configuration = configuration;
            _dateTimeProvider = dateTimeProvider;
        }
        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            var authClaims = new List<Claim>
            {
                //new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
               issuer: _configuration["JWT:ValidIssuer"],
               audience: _configuration["JWT:ValidAudience"],
               expires: _dateTimeProvider.UtcNow.AddDays(1),
               claims: authClaims,
               signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
           );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
