using Extension.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Shared.Entities.Security
{
    public class AuthenticatedUser
    {
        #region [ Properties ]

        public Guid AuthenticationId { get; set; }

        public Guid UserId { get; set; }

        public Guid PersonId { get; set; }

        public string PushNotificationCode { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string Company { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationAt { get; set; }

        public DateTime LastCheck { get; set; }

        public string ThumbnailPhoto { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool IsAdd { get; set; }

        #endregion

        #region [ Calculate Properties ]

        public bool IsExpired => ExpirationAt < DateTime.Now;

        #endregion

        #region [ Public Methods ]

        public void BuildToken(string jwtKey)
        {
            this.ExpirationAt = DateTime.Now.AddMinutes(600);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, this.UserName),
                new Claim("UserId", this.UserId.ToString()),
                new Claim("PushNotificationCode", this.PushNotificationCode.IfNullOrWhiteSpace(String.Empty).ToString()),
                new Claim("Department", this.Department ?? string.Empty),
                new Claim("Company", this.Company ?? string.Empty),
                new Claim("Name", this.Name ?? string.Empty),
                new Claim("ExpirationAt", this.ExpirationAt.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, this.AuthenticationId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? "5838DB85BB629EF3633891852ACF190DF7D98342"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: this.ExpirationAt,
               signingCredentials: creds);

            this.Token = new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}