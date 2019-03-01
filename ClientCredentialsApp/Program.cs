using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientCredentialsApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ClientCredentials",
                ClientSecret = "secretewwr",
                Scope = "api1",
            });

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var reponse = await apiClient.GetAsync("http://localhost:5002/identity");
            if (!reponse.IsSuccessStatusCode)
            {
                Console.WriteLine(reponse.StatusCode);
            }
            else
            {
                var content = await reponse.Content.ReadAsStringAsync();
            }



            Console.ReadKey();
        }
    }
}
