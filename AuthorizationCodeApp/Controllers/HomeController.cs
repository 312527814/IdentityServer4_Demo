using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthorizationCodeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AuthorizationCodeApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task Index()
        {
            var token = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var idToken = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var refreshToken = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            var RedirectUri = await this.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RedirectUri);
            View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
