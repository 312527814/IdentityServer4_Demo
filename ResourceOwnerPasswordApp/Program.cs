using IdentityModel.Client;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResourceOwnerPasswordApp
{
    class Program
    {
        private string AcessToke = "";
        private string RefreshToken = "";
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
                ClientSecret = "secretResourceOwnerPassword2",
                Scope = "api1 openid profile email offline_access roles",
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

            //刷新token 获取accessToken 
            var accessToken = await requestAccessToken(tokenResponse.RefreshToken);

            Console.ReadKey();
        }


        private static async Task<string> requestAccessToken(string refreshToken)
        {
            var apiClient = new HttpClient();
            var disco = await apiClient.GetDiscoveryDocumentAsync("http://localhost:5000/");
            var respone = await apiClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ResourceOwnerPassword",
                ClientSecret = "secretResourceOwnerPassword",
                Scope = "api1 openid profile email offline_access roles",
                RefreshToken = refreshToken
            });
            return respone.AccessToken;
        }
    }
}
