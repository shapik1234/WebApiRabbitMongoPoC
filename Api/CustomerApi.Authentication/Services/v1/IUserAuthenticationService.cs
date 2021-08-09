using CustomerApi.Authentication.Models.v1;
using CustomerApi.Domain.Entities;
using System.Threading;

namespace CustomerApi.Authentication.Services.v1
{
    public interface IUserAuthenticationService
    {
        AuthenticateResponseModel Authenticate(AuthenticateRequestModel model, CancellationToken cancellationToken = default);
        User GetById(int id, CancellationToken cancellationToken = default);
    }
}
