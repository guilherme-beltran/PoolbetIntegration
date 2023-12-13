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
    }
}
