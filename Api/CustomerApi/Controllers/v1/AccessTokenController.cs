using AutoMapper;
using CustomerApi.Authentication.Models.v1;
using CustomerApi.Service.v1.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Controllers.v1
{
	[Produces("application/json")]
	[Route("v1/[controller]")]
	[ApiController]
	public class AccessTokenController : ControllerBase
	{
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public AccessTokenController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        /// <summary>
        /// Action to get Jwt token.
        /// </summary>
        /// <returns>Authenticate user to get access to Customer API</returns>
        /// <response code="200">Retuned if authentification is done</response>
        /// <response code="400">Returned if the authentification couldn't be completed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestModel model)
        {
            try
            {
                AuthenticateResponseModel response = await mediator.Send(new AuthenticateUserCommand()
                {
                    AuthenticateRequest = model
                });

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                else
                    return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
