using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Authentication.Models.v1
{
    public class AuthenticateRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
