using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.AuthenticationEvents;
using Microsoft.Azure.WebJobs.Extensions.AuthenticationEvents.TokenIssuanceStart;
using Microsoft.Extensions.Logging;

// WebJobsAuthenticationEventsTrigger does not yet support ISOLATED model.
// see https://github.com/Azure/azure-sdk-for-net/issues/44950
namespace EntraIDCustomAuthenticationExtensionsIsolated;

public class CustomClaimExtensionFunction
{
    private readonly ILogger<CustomClaimExtensionFunction> _logger;

    public CustomClaimExtensionFunction(ILogger<CustomClaimExtensionFunction> logger)
    {
        _logger = logger;
    }

    [FunctionName("onTokenIssuanceStart")]
    public WebJobsAuthenticationEventResponse Run([WebJobsAuthenticationEventsTrigger] WebJobsTokenIssuanceStartRequest request)
    {
        try
        {
            // Checks if the request is successful and did the token validation pass
            if (request.RequestStatus == WebJobsAuthenticationEventsRequestStatusType.Successful)
            {
                // Fetches information about the user from external data store
                // Add new claims to the token's response
                request.Response.Actions.Add(
                    new WebJobsProvideClaimsForToken(
                        new WebJobsAuthenticationEventsTokenClaim("dateOfBirth", "01/01/2000"),
                        new WebJobsAuthenticationEventsTokenClaim("customRoles", "Writer", "Editor"),
                        new WebJobsAuthenticationEventsTokenClaim("apiVersion", "1.0.0"),
                        new WebJobsAuthenticationEventsTokenClaim("correlationId", request.Data.AuthenticationContext.CorrelationId.ToString())));
            }
            else
            {
                // If the request fails, such as in token validation, output the failed request status, 
                // such as in token validation or response validation.
                _logger.LogInformation(request.StatusMessage);
            }
            return request.Completed();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while processing the custom claim extension");
            return request.Failed(ex);
        }
    }
}