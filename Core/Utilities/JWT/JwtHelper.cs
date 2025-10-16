using Core.Entities;
using Core.Extentions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        public JwtHelper(TokenOptions tokenOptions)
        {
            this._tokenOptions = tokenOptions;
        }

        public AccessToken CreateToken(BaseUser user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.EncryptedFirstName} {user.EncryptedLastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Role", UserTypeExtentions.ToDecriptionString(user.Role)) // Custom claim for user role
            };

            //Her bir izni(operation claim) JWT'ye "role" claim'i olarak ekliyoruz.
            claims.AddRange(operationClaims.Select(claim => new Claim(ClaimTypes.Role, claim.Name)));

            //Güvenlik anahtarı oluşturma
            // TokenOptions'tan gelen SecurityKey'i byte dizisine çevirir.
            var securityKey = Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey);
            var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(securityKey), // Anahtarı ve şifreleme algoritmasını belirtiriz
                    SecurityAlgorithms.HmacSha256Signature
            );

            //Token ayrıntılarını ayarlama
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Kullanıcı bilgileri ve izinler
                Issuer = _tokenOptions.Issuer, // Token'ı veren
                Audience = _tokenOptions.Audience, // Token'ı kullanacak olan

                Expires = DateTime.UtcNow.AddSeconds(_tokenOptions.ExpirationTime), //Eğer expritationTime saniye cinsinden tutuluyorsa DateTime.Now ekleriz
                SigningCredentials = signingCredentials // Güvenlik anahtarı ve algoritma
            };

            //Token'ı yazma ve oluşturöa
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            //AccessToken DTO'sunu Dönme
            return new AccessToken
            {
                Token = tokenHandler.WriteToken(securityToken),
                ExpirationTime = securityToken.ValidTo,

            };
        }
    }
}
