using System.Text.Json.Serialization;

namespace EntraIDCustomAuthenticationExtensionsIsolated.Models;

public class AuthenticationExtensionTokenAction
{
    [JsonPropertyName("@odata.type")]
    public string ODataType => "microsoft.graph.tokenIssuanceStart.provideClaimsForToken";

    public AuthenticationExtensionClaim Claim { get; } = new();
}