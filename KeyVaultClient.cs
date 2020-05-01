using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashCourse.Helpers
{
   
    public static class KeyVaultClient
    {
        private IConfiguration _config { get; }
        private string _tenantId;
        private string _appId;
        private string _appSecret;
        private string _keyValultName;
        private Dictionary<string, string> ConfigurationCache = new Dictionary<string, string>();

        public KeyVaultConfig(IConfiguration config)
        {
            _config = config;
            _tenantId = _config["Authentication:tenant_id"].ToString();
            _appId = _config["Authentication:api_client_id"].ToString();
            _appSecret = _config["Authentication:api_client_secret"].ToString();
            _keyValultName = _config["Authentication:KeyValultName"].ToString();
        }


        private static static async Task<string> GetAccessToken(string azureTenantId, string azureAppId, string azureSecretKey)
        {

            var context = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext($"https://login.windows.net/{_tenantId}");
            var clientCredential = new ClientCredential(_appId, _appSecret);
            var tokenResponse = await context.AcquireTokenAsync("https://vault.azure.net", clientCredential);
            var accessToken = tokenResponse.AccessToken;
            return accessToken;
        }

        private static SecretBundle GetSecret(string secretName)
        {
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessToken));
            var secretBundle = keyVaultClient.GetSecretAsync($"https://{_keyValultName}.vault.azure.net/secrets/{secretName}").GetAwaiter().GetResult();
            return secretBundle;
        }


       
        public static string GetDBConnectionString(string secret)
        {

            var secretKV = GetSecret(secret);
            value = secretKV.Value;

            return value;
        }

    }
}
