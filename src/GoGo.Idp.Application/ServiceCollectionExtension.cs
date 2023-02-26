
using GoGo.Idp.Application.Contracts;
using GoGo.Idp.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GoGo.Idp.Application
{
    public static class ServiceCollectionExtension
    {
        public static void AddAppExtension(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services.AddScoped<IUserService, UserService>();
        }
    }
}