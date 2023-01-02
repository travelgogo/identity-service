using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

namespace GoGo.Idp.IdpServer.GrantValidators
{
    public class OperatorGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => "operator";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            await Task.Yield();
            var grantResult = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Member not found");
            var userName = context.Request.Raw.Get("username");
            var password = context.Request.Raw.Get("password");
            if(userName == "username" && password == "password")
            {
                var user = new User
                {
                    Id = 1,
                    Email = userName,
                    FirstName = "Muhamed",
                    LastName = "Shadi"
                };
                grantResult = new GrantValidationResult(user.Id.ToString(),"oidc", IssueUserClaims(user));
            }
            context.Result = grantResult;
        }

        private static Claim[] IssueUserClaims(User user, bool isMemberLogin = false, bool isSocialLogin = false)
        {

            if (isMemberLogin)
            {
                return new Claim[] 
                {
                    new Claim("email", user.Email ?? ""),
                    new Claim("firstName", user.FirstName ?? ""),
                    new Claim("lastName", user.LastName ?? ""),
                };
            }
            else
            {
                // operator login
                return new Claim[] 
                {
                    new Claim("email", user.Email ?? ""),
                    new Claim("firstName", user.FirstName ?? ""),
                    new Claim("lastName", user.LastName ?? ""),
                };
            }
        } 
    }    
}