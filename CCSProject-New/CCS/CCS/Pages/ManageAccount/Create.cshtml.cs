using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CCS.Models.ViewModels;
using CCS.Models;
using Microsoft.AspNetCore.Identity;

namespace CCS.Pages.ManageAccount
{
    public class CreateModel : PageModel
    {
        private UserManager<CCSUser> userManager;

        public CreateModel(UserManager<CCSUser> usrMgr)
        {
            userManager = usrMgr;
        }

        [BindProperty]
        public CreateUser NewUser { get; set; }


        public PageResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CCSUser user = new CCSUser
                {
                    FirstName = NewUser.FirstName,
                    LastName = NewUser.LastName,
                    UserName = NewUser.UserName,
                };
                IdentityResult result = await userManager.CreateAsync(user, NewUser.Password);
                if(result.Succeeded)
                {
                    return RedirectToPage("./Index");
                } else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return Page();
        }
    }
}