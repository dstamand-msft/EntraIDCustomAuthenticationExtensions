using System.Text.Json;
using EntraIDCustomAuthenticationExtensionsIsolated.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EntraIDCustomAuthenticationExtensionsIsolated;

public class CustomClaimExtensionFunction
{
    private readonly ILogger<CustomClaimExtensionFunction> _logger;

    public CustomClaimExtensionFunction(ILogger<CustomClaimExtensionFunction> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Processes an HTTP POST request to initiate the token issuance flow and returns a response containing token
    /// claims and related data. See
    /// <see href="https://learn.microsoft.com/en-us/entra/identity-platform/custom-claims-provider-reference?toc=%2Fentra%2Fexternal-id%2Ftoc.json&bc=%2Fentra%2Fexternal-id%2Fbreadcrumb%2Ftoc.json#request-to-the-rest-api">
    /// Microsoft Entra documentation</see> for details.
    /// </summary>
    /// <remarks>
    /// This function is triggered by an HTTP POST request and expects a specific JSON structure in the request body.
    /// The response includes claims such as correlation ID, API version, custom roles, and date of birth, which are
    /// used in the token issuance process.
    /// </remarks>
    /// <param name="req">
    /// The HTTP request containing the token issuance data. Must be a POST request with a JSON body that includes
    /// authentication context information.
    /// </param>
    /// <returns>
    /// A JSON result containing the token issuance start response data, including the correlation ID and predefined
    /// claims.
    /// </returns>
    [Function("onTokenIssuanceStart")]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "POST")] HttpRequest req)
    {
        _logger.LogInformation("Processing token issuance start request.");
        var body = await JsonDocument.ParseAsync(req.Body);

        var dataElement = body.RootElement.GetProperty("data");
        var authenticationContextElement = dataElement.GetProperty("authenticationContext");
        var userElement = authenticationContextElement.GetProperty("user");

        _logger.LogDebug("Augmenting claims for user: {displayName} ({userPrincipalName}) [{objectId}]",
            userElement.GetProperty("displayName").ToString(),
            userElement.GetProperty("userPrincipalName").ToString(),
            userElement.GetProperty("id").ToString());

        string correlationId = authenticationContextElement.GetProperty("correlationId").GetString()!;

        var response = new TokenIssuanceStartResponseData();
        var tokenAction = new AuthenticationExtensionTokenAction();
        tokenAction.Claim.CorrelationId = correlationId;
        tokenAction.Claim.ApiVersion = "1.0.0";
        tokenAction.Claim.CustomRoles = new List<string> { "Editor", "AI Song Writer" };
        tokenAction.Claim.DateOfBirth = new DateTime(1990, 1, 1).ToString("yyyy-MM-dd");
        response.Actions.Add(tokenAction);

        _logger.LogInformation("Token issuance start request processed successfully.");
        return new JsonResult(response);
    }
}