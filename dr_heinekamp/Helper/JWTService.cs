using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dr_heinekamp.Helper
{
    // This class responsible for Generate and Verify tokne 
    public class JWTService
    {
        // Field that we need to haave access to the IConfiguration
        private readonly IConfiguration _configuration;

        // This string property should be contain long lenght 
        private string securekey = "This is very important secure key";

        // Constructor
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Generate a token
        public string Generate(string id)
        {
            var symmetricSecureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securekey));
            var credentials = new SigningCredentials(symmetricSecureKey, algorithm: SecurityAlgorithms.HmacSha256Signature);
            var payload = new JwtPayload(id.ToString(), audience: null, claims: null, notBefore: null, expires: DateTime.Today.AddDays(1));
            var header = new JwtHeader(credentials);
            var secuirtyToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(secuirtyToken);
        }

        // Verify a token
        public JwtSecurityToken Verify(string jwt)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Tokens:Key"]);

                tokenHandler.ValidateToken(jwt, new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _configuration["Tokens:Audience"],
                    ValidIssuer = _configuration["Tokens:Issuer"],
                    ValidateLifetime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);
                return (JwtSecurityToken)validatedToken;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}