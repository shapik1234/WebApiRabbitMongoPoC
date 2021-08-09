using CustomerApi.Authentication.Models.v1;
using CustomerApi.Authentication.Options.v1;
using CustomerApi.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace CustomerApi.Authentication.Services.v1
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        private readonly AuthenticationServiceConfiguration authenticationSettings;

        public UserAuthenticationService(IOptions<AuthenticationServiceConfiguration> authenticationSettings)
        {
            this.authenticationSettings = authenticationSettings.Value;
        }

        public User GetById(int id, CancellationToken cancellationToken = default)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public AuthenticateResponseModel Authenticate(AuthenticateRequestModel model, CancellationToken cancellationToken = default)
        {
            var user = users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) 
                return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponseModel(user, token);
        }       
       
        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authenticationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
