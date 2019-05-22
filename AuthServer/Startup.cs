using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("k762ynnzhzW7m8CFZ90cYIiQVhD/Ljiraosr03wXfUo="));

            //var creds2 = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
            //var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SecurityKey"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);// SecurityAlgorithms.HmacSha256 = "HS256"
            var IdentityServerBuilder = services.AddIdentityServer().AddDeveloperSigningCredential();//.AddSigningCredential(credential);
            //.AddDeveloperSigningCredential()
            IdentityServerBuilder.AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())

               .AddCustomTokenRequestValidator<MyCustomTokenRequestValidator>()
               //.AddTestUsers(InMemoryConfiguration.Users().ToList())
               //.AddInMemoryClients(InMemoryConfiguration.Clients())
               .AddInMemoryApiResources(InMemoryConfiguration.ApiResources())
               .AddProfileService<MyCustomProfileService>()
               .AddResourceOwnerValidator<MyResourceOwnerPasswordValidator>()
               .AddClientStore<MyClientStore>()
               .AddSecretValidator<MySecretValidator>()
               ;


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.Use(next =>
            {
                return context =>
                {

                    return next(context);
                };
            });
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}
