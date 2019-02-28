using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApi
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
            services.AddMvcCore()
                 .AddAuthorization()
                 .AddJsonFormatters();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = "http://localhost:5000";
                    options.ApiName = "socialnetwork";
                    options.JwtBearerEvents.OnMessageReceived = context =>
                    {
                        return Task.Run(() =>
                        {
                            var token = context.Request.Headers["token"];
                            context.Token = token.FirstOrDefault();
                        });
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(next =>
            {
                return context =>
                {

                    return next(context);
                };
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
