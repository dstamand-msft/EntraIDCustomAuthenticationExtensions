using System.Text.Json.Serialization;

namespace EntraIDCustomAuthenticationExtensionsIsolated.Models;

/// <summary>
/// Represents the response data for the start of a token issuance process, containing the actions to be performed.
/// </summary>
/// <remarks>
/// This class includes a list of actions that can be executed as part of the token issuance process. The OdataType
/// property indicates the type of the response data as defined in the Microsoft Graph API. See
/// https://learn.microsoft.com/en-us/entra/identity-platform/custom-claims-provider-reference?toc=%2Fentra%2Fexternal-id%2Ftoc.json&bc=%2Fentra%2Fexternal-id%2Fbreadcrumb%2Ftoc.json#response-from-the-rest-api
/// </remarks>
public class TokenIssuanceStartResponseData
{
    [JsonPropertyName("@odata.type")]
    public string ODataType => "microsoft.graph.onTokenIssuanceStartResponseData";

    public List<AuthenticationExtensionTokenAction> Actions { get; } = new();
}