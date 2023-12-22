using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PoolbetIntegration.API.Features.Login;
using PoolbetIntegration.API.Features.UserAdmins;
using PoolbetIntegration.API.Services.Poolbet;

namespace PoolbetIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("/login")]
        public async Task<ActionResult> Login([FromServices] IPoolbetServices poolbetServices)
        {
            var request = new LoginRequest();
            var response = await poolbetServices.SendLogin(request);

            var token = response.Token;

            return Ok(token);
        }

        [HttpGet]
        [Route("/balance")]
        public async Task<ActionResult<BalanceResponse>> GetBalance([FromQuery] BalanceRequest request,
                                                                    [FromServices] ICacheUserAdminRepository repository,
                                                                    CancellationToken cancellationToken)
        {
            var userAdmin = await repository.GetBalanceAsync(username: request.Username, email: request.Email, cancellationToken: cancellationToken);
            BalanceResponse response;
            if (userAdmin is null)
            {
                response = new BalanceResponse(status: false, credit: 0.00m, error: $"No users was found with username {request.Username} and email {request.Email}.", key: "LoginController.GetBalance");
                return BadRequest(response);
            }

            response = new BalanceResponse(status: true,
                                           credit: userAdmin.Credit, 
                                           error: "");

            return Ok(response);
        }
    }
}
