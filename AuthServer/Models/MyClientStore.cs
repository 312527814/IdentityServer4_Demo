using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models
{
    public class MyClientStore : IClientStore
    {
        public async Task<Client> FindClientByIdAsync(string clientId)
        {

            return await Task.Run(() =>
              {
                  return InMemoryConfiguration.Clients().FirstOrDefault(s => s.ClientId == clientId);
              });
        }
    }
}
