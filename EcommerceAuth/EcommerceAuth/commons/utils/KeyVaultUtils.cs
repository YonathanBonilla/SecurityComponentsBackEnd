using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace EcommerceAuth.commons.utils
{
    public class KeyVaultUtils : IKeyVaultUtils
    {

        private readonly SecretClient _secretClient;

        public IConfiguration _configuration { get; }

        public KeyVaultUtils(IConfiguration configuration)
        {
            _configuration = configuration;
            var keyVaultEndpoint = new Uri(_configuration.GetSection("KeyVault:VaultUriLogin").Value);
            _secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());
        }


        public async Task<string> GetSecret(string secretName)
        {
                KeyVaultSecret keyValueSecret = _secretClient.GetSecretAsync(secretName).Result;

                return keyValueSecret.Value;
        }
    }
}
