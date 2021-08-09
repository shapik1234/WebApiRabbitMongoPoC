using CustomerApi.Domain.Entities;

namespace CustomerApi.Authentication.Models.v1
{
    public class AuthenticateResponseModel
    {
		public AuthenticateResponseModel() { }

        public AuthenticateResponseModel(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }        
    }
}
