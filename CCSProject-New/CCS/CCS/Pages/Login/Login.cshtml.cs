using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CCS.Data;
using Microsoft.AspNetCore.Mvc;
using CCS.Models;
using CCS.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel; // This is for the [BindProperty], [DisplayName] attributes
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace CCS.Pages.Login
{
    public class LoginModel : PageModel
    {
        private UserManager<CCSUser> userManager;
        private SignInManager<CCSUser> signInManager;

        public LoginModel(UserManager<CCSUser> usrMgr, SignInManager<CCSUser> signinMgr)
        {
            userManager = usrMgr;
            signInManager = signinMgr;
        }

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CCSUser user = await userManager.FindByNameAsync(LoginViewModel.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(user, LoginViewModel.Password, false, false);
                    //(user-object, password, cookie is persistent(remember me option)-false, lock out account if password is correct (account only used on 1 machine at a time)-false)
                    if (result.Succeeded)
                    {
                        return RedirectToPage("/Main");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Invalid username or password");
            }
            return Page();
        }
    }
}