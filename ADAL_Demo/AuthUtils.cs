using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ADAL_Demo
{
    public class AuthUtils
    {

        public static async Task<string> GetTokenInAppService(string resource, HttpRequestMessage req = null, string environmentBaseUrl = "https://login.windows.net/", string tenantId = "common")
        {
            string userToken = (req == null) ? null : req.Headers.GetValues("X-MS-TOKEN-AAD-ID-TOKEN").FirstOrDefault();
            return await GetTokenInAppService(resource, environmentBaseUrl, tenantId, userToken);
        }

        private static async Task<string> GetTokenInAppService(string resource, string environmentBaseUrl, string tenantId, string userToken)
        {
            return await GetToken(resource, environmentBaseUrl, tenantId, GetClientIdInAppService(), GetClientSecretInAppService(), userToken);
        }

        public static async Task<string> GetToken(string resource, string environmentBaseUrl, string tenantId, string clientId, string clientSecret, string userToken = null)
        {
            if (userToken == null)
            {
                return await GetTokenForApplication(resource, environmentBaseUrl, tenantId, clientId, clientSecret);
            }
            else
            {
                return await GetTokenOnBehalfOfUser(resource, environmentBaseUrl, tenantId, clientId, clientSecret, userToken);
            }
        }

        private static async Task<string> GetTokenOnBehalfOfUser(string resource, string environmentBaseUrl, string tenantId, string clientId, string clientSecret, string userToken)
        {
            UserAssertion userAssertion = new UserAssertion(userToken);
            AuthenticationContext ac = new AuthenticationContext(environmentBaseUrl + tenantId);
            AuthenticationResult ar = await ac.AcquireTokenAsync(resource, new ClientCredential(clientId, clientSecret), userAssertion);
            return ar.AccessToken;
        }

        private static async Task<string> GetTokenForApplication(string resource, string environmentBaseUrl, string tenantId, string clientId, string clientSecret)
        {
            AuthenticationContext ac = new AuthenticationContext(environmentBaseUrl + tenantId);
            AuthenticationResult ar = await ac.AcquireTokenAsync(resource, new ClientCredential(clientId, clientSecret));
            return ar.AccessToken;
        }

        private static string GetClientIdInAppService()
        {
            return Environment.GetEnvironmentVariable("WEBSITE_AUTH_CLIENT_ID");
        }

        private static string GetClientSecretInAppService()
        {
            return Environment.GetEnvironmentVariable("WEBSITE_AUTH_CLIENT_SECRET");
        }

    }
}
