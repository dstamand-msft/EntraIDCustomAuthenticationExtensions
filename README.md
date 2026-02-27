# Setup

## local.settings.json
```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_INPROC_NET8_ENABLED": "1",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "AuthenticationEvents__AudienceAppId": "<app_id>",
    // can be found in the Azure portal, under the app registration under endpoints
    "AuthenticationEvents__AuthorityUrl": "https://<tenant_name_>.ciamlogin.com/<tenant_id>",
    // static, as show in https://learn.microsoft.com/en-us/entra/identity-platform/custom-extension-tokenissuancestart-setup?tabs=visual-studio%2Cazure-portal&pivots=nuget-library
    "AuthenticationEvents__AuthorizedPartyAppId": "99045fe1-7639-4a75-9d4a-577b6ca3810f",
    "AuthenticationEvents__BypassTokenValidation": true
  }
}
```

## references:

https://learn.microsoft.com/en-us/entra/identity-platform/custom-extension-tokenissuancestart-setup?tabs=visual-studio%2Cazure-portal&pivots=nuget-library
<br/>
https://learn.microsoft.com/en-us/entra/identity-platform/custom-extension-onattributecollectionsubmit-retrieve-return-data