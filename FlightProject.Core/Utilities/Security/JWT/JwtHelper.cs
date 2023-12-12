using FlightProject.Core.Entities;
using FlightProject.Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace FlightProject.Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration;
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }


        public AccessToken CreateToken(User user)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration

            };
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken
                (
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.NameIdentifier, user.Id.ToString()) }
                );
            return jwt;
        }
    }
}
