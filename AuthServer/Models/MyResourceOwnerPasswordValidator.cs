using IdentityModel;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models
{
    public class MyResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //if (_users.ValidateCredentials(context.UserName, context.Password))
            //{
            //    var user = _users.FindByUsername(context.UserName);
            //    context.Result = new GrantValidationResult(
            //        user.SubjectId ?? throw new ArgumentException("Subject ID not set", nameof(user.SubjectId)),
            //        OidcConstants.AuthenticationMethods.Password, _clock.UtcNow.UtcDateTime,
            //        user.Claims);
            //}

            var user = InMemoryConfiguration.Users().FirstOrDefault(f=>f.Password==context.Password&&f.Username==context.UserName);
            context.Result = new GrantValidationResult(
                user.SubjectId ?? throw new ArgumentException("Subject ID not set", nameof(user.SubjectId)),
                OidcConstants.AuthenticationMethods.Password, DateTime.UtcNow,
                user.Claims);
            return Task.CompletedTask;
        }
    }
}
