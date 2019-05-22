using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models
{
    public class MyCustomTokenRequestValidator : ICustomTokenRequestValidator
    {
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            //context.Result.IsError = true;
            //context.Result.Error = "错误dsada";
            //context.Result.CustomResponse = new Dictionary<string, object> { { "station", "站点" }, { "city", "城市" }, { "province", "省" } };
            return Task.CompletedTask;
        }
    }
}
