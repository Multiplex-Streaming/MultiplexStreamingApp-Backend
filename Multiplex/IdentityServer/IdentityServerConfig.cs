using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Multiplex.IdentityServer
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[] {
                new ApiResource("multiplexAPI", "Multiplex API")
                {
                    UserClaims =
                    {
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Role
                    }
                },
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            var accessTokenLifeTimeDays = 30;
            var refreshTokenLifeTimeDays = 60;
            return new List<Client>
            {
                new Client
                {
                    AllowOfflineAccess = true,
                    ClientId = "multiplex",
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = refreshTokenLifeTimeDays * 24 * 60 * 60,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AccessTokenLifetime = accessTokenLifeTimeDays * 24 * 60 * 60,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    RequireClientSecret = false,
                    AllowedScopes = { "multiplexAPI" }
                }
            };
        }
    }
}
