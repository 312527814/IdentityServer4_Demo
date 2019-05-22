using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                 new IdentityResources.Email(),
                 new IdentityResource("roles","角色",new string[]{ "role"})

            };
        }
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[]
            {
                new ApiResource("socialnetwork", "社交网络"),
                 new ApiResource("api1", "api1"),
            };
        }

        public static IEnumerable<Client> Clients()
        {

            var s = "secretewwr".Sha256();
            return new[]
            {
                new Client
                {
                    ClientId = "ClientCredentials",
                    ClientSecrets ={ new Secret("secretewwr".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //AccessTokenLifetime = 3600,
                    AllowOfflineAccess = true,
                    Claims={
                      new System.Security.Claims.Claim("Role","admin"),
                      new System.Security.Claims.Claim("Name","zhangsan")
                    },
                    AllowedScopes = new [] { "api1", },
                },
                 new Client
                {
                    ClientId = "ResourceOwnerPassword",
                    ClientSecrets = new [] { new Secret("secretResourceOwnerPassword".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess=true,
                    
                    AllowedScopes = new [] {
                        "api1" ,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles"
                    },
                },
                new Client
                {
                    ClientId = "mvc client",
                    ClientName = "ASP.NET Core MVC Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets={ new Secret("mvc secret".Sha256())},
                    RedirectUris = { "http://localhost:52657/signin-oidc" },
                    FrontChannelLogoutUri="http://localhost:52657/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:52657/signout-callback-oidc" },
                    AlwaysIncludeUserClaimsInIdToken=true,
                    AllowOfflineAccess=true,
                    AlwaysSendClientClaims=true,
                    AccessTokenLifetime=30,
                    AllowedScopes = new List<string>
                    {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                         "roles",
                    }
                },
                 new Client
                {
                    ClientId = "angular-clinet",
                    ClientName = "angular SPA 客户端",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    AccessTokenLifetime=60*5,
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                         "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                    }
                },
                new Client
                {
                    ClientId = "mvc_implicit",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:53330/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:53330/signout-callback-oidc" },
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1",
                    }
                },
                 new Client
                {
                    ClientId = "mvc Hybrid",
                    ClientName = "MVC Client Hybrid",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:54902/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:54902/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                }
            };
        }

        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "mail@qq.com",
                    Password = "1",
                    Claims={
                        new System.Security.Claims.Claim(JwtClaimTypes.Email,"312527814@qq.com"),
                         new System.Security.Claims.Claim(JwtClaimTypes.Profile,@"{ 'name': 'One Hacker Way', 'family_name': 'Heidelberg'}"),
                         new System.Security.Claims.Claim("role","管理员")
                    }
                }
            };
        }
    }
}
