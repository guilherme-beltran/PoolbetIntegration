namespace PoolbetIntegration.API.Features.UserAdmins;

public class CheckUserResponse
{
    public bool Status { get; set; }
    public string? Error { get; set; }
    public string? Key { get; set; }

    public CheckUserResponse(bool status, string? error = null)
    {
        Status = status;
        Error = error;
    }
    public CheckUserResponse(bool status, string error, string? key = null)
    {
        Status = status;
        Error = error;
        Key = key;
    }
}
