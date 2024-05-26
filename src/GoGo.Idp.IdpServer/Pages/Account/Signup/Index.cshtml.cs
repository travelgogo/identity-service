using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Test;
using GoGo.Idp.Application.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoGo.Idp.IdpServer.Pages.Signin;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    //private readonly TestUserStore _users;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IEventService _events;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IIdentityProviderStore _identityProviderStore;
    private readonly IUserService _userService;

    public ViewModel View { get; set; }
        
    [BindProperty]
    public InputModel Input { get; set; }
        
    public Index(
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider,
        IIdentityProviderStore identityProviderStore,
        IEventService events,
        IUserService userService)
    {
        // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
        _interaction = interaction;
        _schemeProvider = schemeProvider;
        _identityProviderStore = identityProviderStore;
        _events = events;
        _userService = userService;
    }

    public async Task<IActionResult> OnGet(string returnUrl)
    {
        await Task.Yield();
        // View.ExternalProviders = new List<ViewModel.ExternalProvider>
        // {
        //     new ViewModel.ExternalProvider
        //     {
        //         DisplayName = "Login with Google",
        //         AuthenticationScheme = "google"
        //     },
        //     new ViewModel.ExternalProvider
        //     {
        //         DisplayName = "Login with Azure Active Directory",
        //         AuthenticationScheme = "add"
        //     }
        // };
        // if (View.IsExternalLoginOnly)
        // {
        //     // we only have one option for logging in and it's an external provider
        //     return RedirectToPage("/ExternalLogin/Challenge", new { scheme = View.ExternalLoginScheme, returnUrl });
        // }

        return Page();
    }
        
    public async Task<IActionResult> OnPost()
    {
        // check if we are in the context of an authorization request
        //var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        // the user clicked the "cancel" button
        if (Input.Button != "signin")
        {
            return Redirect("~/");
        }

        if (ModelState.IsValid)
        {
            var res = await _userService.CreateUserAccount(new Application.Models.UserInfo
            {
                Email = Input.Username,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Password = Input.Password
            });

            if (res)
            {
                return Redirect("~/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot create user");
                return Page();
            }
            
        }
        ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);
        return Page();
    }
    
}