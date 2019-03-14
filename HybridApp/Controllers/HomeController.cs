using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HybridApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace HybridApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            var token = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var idToken = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var refreshToken = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            var RedirectUri = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RedirectUri);
            var result = await reqeusetResuoureServer(token);

            if (result == HttpStatusCode.Unauthorized)
            {
                token = await requestAccessToken(refreshToken);
                await reqeusetResuoureServer(token);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private async Task<HttpStatusCode> reqeusetResuoureServer(string accessToken)
        {
            //var accessToken = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken);
            var reponse = await apiClient.GetAsync("http://localhost:5002/identity");
            return reponse.StatusCode;
        }

        private async Task<string> requestAccessToken(string refreshToken)
        {
            //var refreshToken = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            var apiClient = new HttpClient();
            var disco = await apiClient.GetDiscoveryDocumentAsync("http://localhost:5000/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return "";
            }
            var respone = await apiClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "mvc client",
                ClientSecret = "mvc secret",
                Scope = "api openid",
                //GrantType = "code",
                RefreshToken = refreshToken
            });
            return respone.AccessToken;
        }
    }
}
