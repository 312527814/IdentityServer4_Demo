using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResourceOwnerPasswordApp
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
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ResourceOwnerPassword",
                ClientSecret = "secretResourceOwnerPassword",
                Scope = "api1 openid profile email roles",
                UserName = "mail@qq.com",
                Password = "1",

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

            var apiClient2 = new HttpClient();
            apiClient2.SetBearerToken(tokenResponse.AccessToken);
            var reponse2 = await apiClient2.GetAsync(disco.UserInfoEndpoint);
            if (!reponse2.IsSuccessStatusCode)
            {
                Console.WriteLine(reponse2.StatusCode);
            }
            else
            {
                var content = await reponse2.Content.ReadAsStringAsync();
            }



            Console.ReadKey();
        }
    }
}
