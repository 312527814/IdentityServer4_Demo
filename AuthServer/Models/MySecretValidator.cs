using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models
{
    public class MySecretValidator : ISecretValidator
    {
        public async Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
        {
            return await Task.Run(() =>
            {
                var client = InMemoryConfiguration.Clients().FirstOrDefault(s => s.ClientId == parsedSecret.Id);
                if (client != null)
                {
                    foreach (var item in client.ClientSecrets)
                    {
                        //验证逻辑 自定义
                        if (item.Value == parsedSecret.Credential.ToString())
                        {
                            return new SecretValidationResult { Success = true };
                        }
                    }
                    // client.ClientSecrets == parsedSecret.Credential;
                }
                return new SecretValidationResult { Success = false };
            });
        }
    }
}
