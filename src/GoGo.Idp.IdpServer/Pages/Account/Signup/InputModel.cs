// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace GoGo.Idp.IdpServer.Pages.Signin;

public class InputModel
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Username { get; set; }
        
    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
        
    public bool RememberLogin { get; set; }
        
    public string ReturnUrl { get; set; }

    public string Button { get; set; }
}