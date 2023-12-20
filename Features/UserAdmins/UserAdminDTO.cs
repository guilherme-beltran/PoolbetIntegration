namespace PoolbetIntegration.API.Features.UserAdmins;

public class UserAdminDTO 
{
    public int UserAdminId { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Credit { get; set; }

    public UserAdmin ToEntity()
    {
        return new UserAdmin
        {
            UserAdminId = UserAdminId,
            Name = Name,
            Email = Email,
            Username = Username,
            Credit = Credit,
        };
    }
}
