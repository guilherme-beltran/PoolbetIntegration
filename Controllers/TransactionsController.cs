using Microsoft.AspNetCore.Mvc;
using PoolbetIntegration.API.Features.Transactions;
using PoolbetIntegration.API.Features.UserAdmins;

namespace PoolbetIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public async Task<ActionResult<IEnumerable<UserAdmin>>> GetAll([FromServices] ICacheUserAdminRepository repository,
                                                                       CancellationToken cancellationToken)
        {
            var usersAdmin = await repository.GetAllAsync(cancellationToken);
            return Ok(usersAdmin);
        }

        [HttpGet]
        [Route("/{userId}")]
        public async Task<ActionResult<IEnumerable<UserAdmin>>> GetById([FromServices] ICacheUserAdminRepository repository,
                                                                        int userId)
        {
            var userAdmin = await repository.GetByIdAsync(userId);
            if (userAdmin is null)
                return NotFound($"Usuario com id {userId} não encontrado.");

            return Ok(userAdmin);
        }


        [HttpPost]
        [Route("/set-transaction")]
        public async Task<ActionResult<TransactionResponse>> SetTransaction([FromBody] TransactionRequest request,
                                                       [FromServices] ICacheUserAdminRepository repository,
                                                       CancellationToken cancellationToken)
        {
            var response = await repository.UpdateBalance(value: request.Value,
                                                          type: request.Type,
                                                          username: request.Username,
                                                          email: request.Email,
                                                          betId: request.BetId,
                                                          cancellationToken: cancellationToken);

            if (response.Status == false)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
