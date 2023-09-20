using ForoEnLineaApi.Interfaces.Repositories;
using ForoEnLineaApi.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForoEnLineaApi.Infrasestructure
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;

        public JwtService(IOptions<JwtSettings> jwtSettings, IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
            _jwtSettings = jwtSettings.Value;
        }

        public string Generate(Claim[] claims, DateTime? expiresUtc = null, string audience = null)
        {
            var symmetricSecuityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecuityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: audience, //ver esto
                claims: claims,
                expires: expiresUtc ?? _dateTimeService.NowUtc.AddSeconds(_jwtSettings.ExpiresInSeconds),
                signingCredentials: signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
