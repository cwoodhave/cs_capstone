using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCS.Data;
using CCS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CCS.Pages.ManageAccount
{
    public class ProfileModel : PageModel
    {
        private UserManager<CCSUser> userManager;
        private SignInManager<CCSUser> signInManager;

        public ProfileModel(UserManager<CCSUser> usrMgr, SignInManager<CCSUser> sgnMgr)
        {
            userManager = usrMgr;
            signInManager = sgnMgr;
        }

        //[BindProperty]
        public CCSUser CCSUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (signInManager.IsSignedIn(User))
            {
                CCSUser = await userManager.GetUserAsync(User);
                return Page();
            }

            return RedirectToPage("/Login/Login");
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync(string newFirstName, string newLastName, string newUserName)
        {
            CCSUser = await userManager.GetUserAsync(User);

            if (string.IsNullOrEmpty(newUserName))
            {
                ModelState.AddModelError("newUserName", "UserName is Required!");
            }
            else
            {
                CCSUser.FirstName = newFirstName;
                CCSUser.LastName = newLastName;
                CCSUser.UserName = newUserName;

                IdentityResult result = await userManager.UpdateAsync(CCSUser);
                if (result.Succeeded)
                {
                    await userManager.UpdateAsync(CCSUser);
                    await signInManager.RefreshSignInAsync(CCSUser);
                    ViewData["AlertMessage"] = "Successfully updated user profile";
                    return Page();
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync(string oldPassword, string newPassword, string newPasswordVerify)
        {
            CCSUser = await userManager.GetUserAsync(User);

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(newPasswordVerify))
            {
                ModelState.AddModelError("password", "All Password fields are required to change password");
            }
            else if (newPassword != newPasswordVerify)
            {
                ModelState.AddModelError("password", "New passwords do not match");
            }
            else
            {

                IdentityResult result = await userManager.ChangePasswordAsync(CCSUser, oldPassword, newPassword);
                if (result.Succeeded)
                {
                    ViewData["AlertMessage"] = "Successfully updated user password";
                    return Page();
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return Page();
        }
    }
}