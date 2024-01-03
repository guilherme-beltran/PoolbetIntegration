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
                                                                    [FromServices] IBalanceHandler handler,
                                                                    CancellationToken cancellationToken)
        {
            var response = await handler.Handle(request, cancellationToken);
            
            if (response.Status is false && response.Key == "convertedCredit")
                return StatusCode(500, response);
            
            if (response.Status is false)
                return BadRequest(response);

            return Ok(response);

        }

        [HttpPost]
        [Route("/check-user")]
        public async Task<ActionResult<CheckUserResponse>> CheckUser([FromBody] CheckUserRequest request, [FromServices] ICacheUserAdminRepository repository, CancellationToken cancellationToken)
        {
            var response = await repository.IsUserCorrect(username: request.Login, email: request.Email, id: request.UserId, cancellationToken: cancellationToken);
            if (response.Status is false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
