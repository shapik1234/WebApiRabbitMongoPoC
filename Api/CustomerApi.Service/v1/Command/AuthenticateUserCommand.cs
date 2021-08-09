using CustomerApi.Authentication.Models.v1;
using MediatR;

namespace CustomerApi.Service.v1.Command
{
    public class AuthenticateUserCommand : IRequest<AuthenticateResponseModel>
    {
        public AuthenticateRequestModel AuthenticateRequest { get; set; }
    }
}
