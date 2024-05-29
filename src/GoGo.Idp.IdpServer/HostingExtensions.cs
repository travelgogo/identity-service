using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using GoGo.Idp.IdpServer.GrantValidators;
using GoGo.Idp.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;

namespace GoGo.Idp.IdpServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
        builder.Services.AddIdentityServer(options =>
            {
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                //options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration["Azure:SqlServer:IdentityConnection"],
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration["Azure:SqlServer:IdentityConnection"],
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            });
            //.AddTestUsers(TestUsers.Users);
        // .AddExtensionGrantValidator<MemberGrantValidator>()
        // .AddExtensionGrantValidator<OperatorGrantValidator>();

        builder.Services.AddAuthentication()
            .AddGoogle("google", options => 
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.ClientId = "1068885405661-ul1r2m37249fr1i5gvhnvqbvvdrnfmso.apps.googleusercontent.com";
                options.ClientSecret = "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";
                //options.Events.OnRemoteFailure = Handleremove
            });
            // .AddOpenIdConnect("google", "Google", options =>
            // {
            //     options.Authority = "https://accounts.google.com/o/oauth2/auth";
            //     options.ClientId = "1068885405661-ul1r2m37249fr1i5gvhnvqbvvdrnfmso.apps.googleusercontent.com";
            //     options.ClientSecret = "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";
            //     options.ResponseType = OpenIdConnectResponseType.Code;
            //     //options.cal
            // });
        IdentityModelEventSource.ShowPII = true;
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        InitializeDatabase(app);
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();
        
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }

    private static void InitializeDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        context.Database.Migrate();
        if (!context.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Config.IdentityResources)
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in Config.ApiScopes)
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiResources.Any())
        {
            foreach (var resource in Config.ApiResources)
            {
                context.ApiResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        var userContext = serviceScope.ServiceProvider.GetRequiredService<IdentityContext>();
        userContext.Database.Migrate();
        if (!userContext.Roles.Any())
        {
            foreach (var resource in Config.Roles)
            {
                userContext.Roles.Add(resource);
            }
            userContext.SaveChanges();
        }

        if (!userContext.Users.Any())
        {
            foreach (var resource in Config.Users)
            {
                userContext.Users.Add(resource);
            }
            userContext.SaveChanges();
        }
    }

}
