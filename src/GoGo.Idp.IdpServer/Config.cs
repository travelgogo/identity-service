using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using GoGo.Idp.Domain.Entities;
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
                Description = "Can access product data",
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
                    JwtClaimTypes.Profile,
                    //JwtClaimTypes.Audience
                }
            },
            new ApiScope 
            {
                Name = "payment-data",
                DisplayName = "payment-data",
                Description = "Can access payment data",
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
                Name = "member-data",
                DisplayName = "member-data",
                Description = "Can access member data",
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

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("invoice", "Invoice API")
        {
            Scopes = { "invoice.read", "invoice.pay", "manage", "enumerate" }
        },
        
        new ApiResource("product-data", "api resource product-data")
        {
            Scopes = { "payment-data", "product-data", "manage", "enumerate" }
        }
    };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "L324F765-8D21-459V-HJ23-V32D9820962A",
                ClientName = "Product service",
                AllowedGrantTypes =  GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                AllowedScopes = { "product-data", "payment-data" },
            },
            new Client
            {
                ClientId = "FA82DF09-T5D3-SA35-LG7D-H5S2FL6DS335",
                ClientName = "Payment service",
                AllowedGrantTypes =  GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("A25D23A5-F569-5213-8FA2-1A52SDC4AJ44".Sha256()) },
                AllowedScopes = { "payment-data" },
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
            },
            new Client
            {
                ClientId = "TD24G6DD-G51T-22DD-KG21-F2F5G52FF6RT",
                ClientName = "Razor management",
                ClientSecrets = { new Secret("51GHR2C4-6SD3-6HE3-443F-G4S52EW2DA1D".Sha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RedirectUris = { "https://localhost:5050/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:5050/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:5050/signout-oidc" },
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "product-data"
                },
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowedCorsOrigins = {"https://localhost:5050"}
            }
        };

    public static IEnumerable<Role> Roles => new Role[]
    {
        new Role
        {
            Name = "User",
            IsActive = true,
            Description = "User"
        },

        new Role
        {
            Name = "Admin",
            IsActive = true,
            Description = "Admin"
        },

        new Role
        {
            Name = "Head Office",
            IsActive = true,
            Description = "Head Office"
        },
    };

    public static IEnumerable<User> Users => new User[]
    {
        new User
        {
            FirstName = "Bob",
            LastName = "Nguyen",
            Email = "user@gmail.com",
            PasswordHash = "AQAAAAIAAYagAAAAEF1yakGMB29YUJTaQsc29G9Pq9wA13uZC7N5GJbyVV4ONrHUkMuTltI3lIiS1mHzKA==",
            CreatedUtc = DateTime.UtcNow,
            IsActive = true,
            UserRoles = new List<UserRole>
            {
                new() 
                {
                    RoleId = 1,
                    IsActive = true
                }
            },
            UserClaims = new List<UserClaim>
            {
                new()
                {
                    ClaimType = JwtClaimTypes.GivenName,
                    ClaimValue = "Bob"
                },
                new()
                {
                    ClaimType = JwtClaimTypes.FamilyName, //System.Security.Claims.ClaimTypes.GivenName,
                    ClaimValue = "Nguyen"
                },
                new()
                {
                    ClaimType = JwtClaimTypes.Email,
                    ClaimValue = "user@gmail.com"
                },
                new()
                {
                    ClaimType = JwtClaimTypes.Role,
                    ClaimValue = "user"
                }
            }
        },
        new User
        {
            FirstName = "Doe",
            LastName = "Nguyen",
            Email = "admin@gmail.com",
            PasswordHash = "AQAAAAIAAYagAAAAEJHO8lBufGJ6oaYwyBZgwRTCnCNpcTxE4dOc8IhcqTatBr45r//pDOt71pOdvQqQ2A==",
            CreatedUtc = DateTime.UtcNow,
            IsActive = true,
            UserRoles = new List<UserRole>
            {
                new() 
                {
                    RoleId = 2,
                    IsActive = true
                }
            },
            UserClaims = new List<UserClaim>
            {
                new()
                {
                    ClaimType = JwtClaimTypes.GivenName,
                    ClaimValue = "Doe"
                },
                new()
                {
                    ClaimType = JwtClaimTypes.FamilyName,
                    ClaimValue = "Nguyen"
                },
                new()
                {
                    ClaimType = System.Security.Claims.ClaimTypes.Email,
                    ClaimValue = "user@gmail.com"
                },
                new()
                {
                    ClaimType = System.Security.Claims.ClaimTypes.Role,
                    ClaimValue = "admin"
                },
                new()
                {
                    ClaimType = System.Security.Claims.ClaimTypes.Sid,
                    ClaimValue = "2"
                }
            }
        },
    };
}