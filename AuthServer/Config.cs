using System.Collections.Generic;
using IdentityServer4.Models;

namespace AuthServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1r", "API #1")
                {
                    Scopes = {new Scope("api1", "Full access to API #1") }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client {
                    ClientId = "demo_api_swagger",
                    ClientName = "Swagger UI for demo_api",
                    ClientSecrets = {new Secret("secret".Sha256())}, // change me!
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:44361/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:44361"},
                    AllowedScopes = {"api1"},
                    //RequireConsent = true,
                    //AllowedGrantTypes = GrantTypes.Implicit,
                    //AllowedScopes = { "openid", "profile", "email", "api.read" },
                    //RedirectUris = {"http://localhost:4200/auth-callback"},
                    //PostLogoutRedirectUris = {"http://localhost:4200/"},
                    //AllowedCorsOrigins = {"http://localhost:4200"},
                    //AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 86400
                },
                new Client {
                    ClientId = "unit_test_client",
                    ClientName = "unit test client for MonstersInc",
                    ClientSecrets = {new Secret("secret".Sha256())}, // change me!
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:44361/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:44361"},
                    AllowedScopes = {"api1"},
                    //RequireConsent = true,
                    //AllowedGrantTypes = GrantTypes.Implicit,
                    //AllowedScopes = { "openid", "profile", "email", "api.read" },
                    //RedirectUris = {"http://localhost:4200/auth-callback"},
                    //PostLogoutRedirectUris = {"http://localhost:4200/"},
                    //AllowedCorsOrigins = {"http://localhost:4200"},
                    //AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 86400
                }

            };
        }
    }
}
