using Newtonsoft.Json;

namespace PoolbetIntegration.API.Features;

public sealed class User
{
    [JsonProperty("name")]
    public string? Name;

    [JsonProperty("lastName")]
    public string? LastName;

    [JsonProperty("email")]
    public string? Email;

    [JsonProperty("login")]
    public string? Login;

    [JsonProperty("password")]
    public string? Password;

    [JsonProperty("deleted")]
    public bool? Deleted;

    [JsonProperty("firstLogin")]
    public bool? FirstLogin;

    [JsonProperty("commission")]
    public int? Commission;

    [JsonProperty("parentId")]
    public int? ParentId;

    [JsonProperty("jurisdictionId")]
    public int? JurisdictionId;

    [JsonProperty("registerDateTime")]
    public DateTime? RegisterDateTime;

    [JsonProperty("country")]
    public string? Country;

    [JsonProperty("city")]
    public string? City;

    [JsonProperty("credit")]
    public double? Credit;

    [JsonProperty("jurisdictionEntityId")]
    public int? JurisdictionEntityId;

    [JsonProperty("jurisdiction")]
    public Jurisdiction? Jurisdiction;

    [JsonProperty("userPermissions")]
    public List<UserPermission?>? UserPermissions;

    [JsonProperty("children")]
    public List<object?>? Children;

    [JsonProperty("isPassword")]
    public bool? IsPassword;

    [JsonProperty("oldPassword")]
    public object? OldPassword;

    [JsonProperty("bank")]
    public string? Bank;

    [JsonProperty("agency")]
    public string? Agency;

    [JsonProperty("account")]
    public string? Account;

    [JsonProperty("accountType")]
    public int? AccountType;

    [JsonProperty("email2")]
    public object? Email2;

    [JsonProperty("email3")]
    public object? Email3;

    [JsonProperty("cellPhone")]
    public object? CellPhone;

    [JsonProperty("phone")]
    public object? Phone;

    [JsonProperty("phone2")]
    public object? Phone2;

    [JsonProperty("gender")]
    public object? Gender;

    [JsonProperty("birthDate")]
    public object? BirthDate;

    [JsonProperty("notes")]
    public object? Notes;

    [JsonProperty("address")]
    public object? Address;

    [JsonProperty("zipCode")]
    public object? ZipCode;

    [JsonProperty("whatsapp")]
    public object? Whatsapp;

    [JsonProperty("contact")]
    public object? Contact;

    [JsonProperty("currencyCode")]
    public string? CurrencyCode;

    [JsonProperty("registrationDate")]
    public DateTime? RegistrationDate;

    [JsonProperty("permits")]
    public List<Permit?>? Permits;

    [JsonProperty("lastLogin")]
    public object? LastLogin;

    [JsonProperty("numberOfLogins")]
    public int? NumberOfLogins;

    [JsonProperty("deposits")]
    public double? Deposits;

    [JsonProperty("withdrawals")]
    public double? Withdrawals;

    [JsonProperty("balance")]
    public double? Balance;

    [JsonProperty("registerDateStr")]
    public string? RegisterDateStr;

    [JsonProperty("selfieWithPaper")]
    public object? SelfieWithPaper;

    [JsonProperty("cnicFront")]
    public object? CnicFront;

    [JsonProperty("access")]
    public bool? Access;

    [JsonProperty("cnicBack")]
    public object? CnicBack;

    [JsonProperty("proofOfAddress")]
    public object? ProofOfAddress;

    [JsonProperty("selfieWithIdentity")]
    public object? SelfieWithIdentity;

    [JsonProperty("accountSummary")]
    public AccountSummary? AccountSummary;

    [JsonProperty("docs")]
    public List<object?>? Docs;

    [JsonProperty("totalBettingAmount")]
    public double? TotalBettingAmount;

    [JsonProperty("bettingWinAmount")]
    public double? BettingWinAmount;

    [JsonProperty("bettingBalance")]
    public double? BettingBalance;

    [JsonProperty("jurisdictionStr")]
    public List<object?>? JurisdictionStr;

    [JsonProperty("minimumValue")]
    public object? MinimumValue;

    [JsonProperty("id")]
    public int? Id;
}
