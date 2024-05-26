
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
        private readonly IPasswordHasher<string> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher<string> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public UserInfo? GetUserAccount(string userName, string password)
        {
            var user = _unitOfWork.Repo<User>().GetAsync(x => x.Email == userName, new string[] {"UserRoles.Role", "UserClaims"}).FirstOrDefault();
            if (user == null)
                return null;
            
            var verify = _passwordHasher.VerifyHashedPassword(userName, user.PasswordHash, password);
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

        public async Task<bool> CreateUserAccount(UserInfo userInfo)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var user = _unitOfWork.Repo<User>().GetAsync(x => x.Email == userInfo.Email, new string[] {"UserRoles.Role", "UserClaims"}).FirstOrDefault();
                if (user != null)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user.Email, userInfo.Password);
                    await _unitOfWork.Repo<User>().UpdateAsync(user);
                }
                else
                {
                    var entity = new User
                    {
                        Email = userInfo.Email,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        PasswordHash = _passwordHasher.HashPassword(userInfo.Email, userInfo.Password),
                        IsActive = true,
                        UserRoles = new List<UserRole>
                        {
                            new() 
                            {
                                RoleId = 1,
                                IsActive = true
                            }
                        }
                    };
                    await _unitOfWork.Repo<User>().AddAsync(entity);
                }
                
                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
            
        }
    }
}