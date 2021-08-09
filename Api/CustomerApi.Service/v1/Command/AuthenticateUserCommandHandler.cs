using CustomerApi.Authentication.Models.v1;
using CustomerApi.Authentication.Services.v1;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Service.v1.Command
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateResponseModel>
    {
        private readonly IUserAuthenticationService authenticationService;

        public AuthenticateUserCommandHandler(IUserAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

		public async Task<AuthenticateResponseModel> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
		{
            return authenticationService.Authenticate(request.AuthenticateRequest, cancellationToken);
        }
	}
}
