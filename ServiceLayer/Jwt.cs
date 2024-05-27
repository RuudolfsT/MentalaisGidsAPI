using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MentalaisGidsAPI.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class Jwt : IJwt
    {
        private readonly string _secret;

        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

        public Jwt(IConfiguration configuration)
        {
            _secret = configuration["Jwt:Secret"]!;
        }

        public string GenerateToken(Lietotajs lietotajs)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_secret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, lietotajs.Lietotajvards),
                new("userid", lietotajs.LietotajsID.ToString())
            };

            // TODO - te vajadzēs mehānismu kā pielikt Lomas (Lomas konvertē par jaunu claim un pievieno 'claims' sarakstam)

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }

        //jāpadomā vai šo vajag
        public int? ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var key = Encoding.UTF8.GetBytes(_secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(
                    token,
                    tokenValidationParams,
                    out SecurityToken validatedToken
                );

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userid").Value);
                return userId;
            }
            catch
            {
                //vienalga kāda iemesla dēļ validācija neizdevās, atgriežam neko
                return null;
            }

            throw new NotImplementedException();
        }
    }
}
