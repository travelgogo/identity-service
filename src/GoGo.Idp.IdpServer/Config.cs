using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace GoGo.Idp.IdpServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope 
            {
                Name = "product-data",
                DisplayName = "product-data",
                Description = "Can get product data",
                Enabled = true,
                UserClaims = 
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Address,
                    JwtClaimTypes.Confirmation,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.Id,
                    JwtClaimTypes.Profile
                }
            },
            new ApiScope 
            {
                Name = "payment-data",
                DisplayName = "payment-data",
                Description = "Can get payment data",
                Enabled = true,
                UserClaims = 
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Address,
                    JwtClaimTypes.Confirmation,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.Id,
                    JwtClaimTypes.Profile
                }
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "L324F765-8D21-459V-HJ23-V32D9820962A",
                ClientName = "Product service",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "product-data", "payment-data" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "H324T436-8D21-459V-HJ23-F2F55103DD71",
                ClientName = "Gogo management ui",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "http://localhost:3000/signin-oidc" },
                FrontChannelLogoutUri = "http://localhost:3000/signout-oidc",
                PostLogoutRedirectUris = { "http://localhost:3000/signout-oidc" },
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "product-data"
                },
                AllowedCorsOrigins = {"http://localhost:3000"}
            }
        };
}