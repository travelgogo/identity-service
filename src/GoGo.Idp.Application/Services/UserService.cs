
using GoGo.Idp.Application.Contracts;
using GoGo.Idp.Application.Models;
using GoGo.Idp.Domain.Entities;
using GoGo.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;

namespace GoGo.Idp.Application.Services
{
    class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public UserInfo? GetUserAccount(string userName, string password)
        {
            var user = _unitOfWork.Repo<User>().GetAsync(x => x.Email == userName, new string[] {"UserRoles.Role", "UserClaims"}).FirstOrDefault();
            if (user == null)
                return null;
            
            var verify = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (user.IsActive && !user.IsRequireChangePassword && verify == PasswordVerificationResult.Success)
            {
                return new UserInfo
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive,
                    IsDeleted = user.IsDeleted,
                    IsRequireChangePassword = user.IsRequireChangePassword,
                    Roles = user.UserRoles.Select(x => new RoleItem
                    {
                        Name = x.Role?.Name,
                    }),
                    Claims = user.UserClaims.Select(x => new ClaimItem
                    {
                        Type = x.ClaimType,
                        Value = x.ClaimValue
                    })
                };
            }

            return null;
        }
    }
}