using System.Text.Json.Serialization;

namespace EntraIDCustomAuthenticationExtensionsIsolated.Models;
public class AuthenticationExtensionClaim
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string CorrelationId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string DateOfBirth { get; set; }

    public string ApiVersion { get; set; }

    public List<string> CustomRoles { get; set; }
}