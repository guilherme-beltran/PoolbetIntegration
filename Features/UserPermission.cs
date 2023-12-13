using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features;

public sealed class UserPermission
{
    [JsonProperty("userAdminId")]
    public int? UserAdminId;

    [JsonProperty("permissionId")]
    public int? PermissionId;

    [JsonProperty("userAdmin")]
    public object? UserAdmin;

    [JsonProperty("permission")]
    public object? Permission;

    [JsonProperty("value")]
    public bool? Value;

    [JsonProperty("id")]
    public int? Id;
}
