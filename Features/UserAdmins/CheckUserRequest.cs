namespace PoolbetIntegration.API.Features.UserAdmins;

public class CheckUserRequest
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
}
