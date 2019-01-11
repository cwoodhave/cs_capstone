using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCS.Data;
using CCS.Models;
using Microsoft.AspNetCore.Identity;

namespace CCS.Pages.ManageAccount
{
    public class IndexModel : PageModel
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<CCSUser> userManager;

        public IndexModel(RoleManager<IdentityRole> roleMgr, UserManager<CCSUser> usrMgr)
        {
            roleManager = roleMgr;
            userManager = usrMgr;
        }

        public List<CCSUser> CCSUsers { get;set; }

        public async Task OnGetAsync()
        {
            CCSUsers = new List<CCSUser>();
            foreach(CCSUser user in userManager.Users)
            {
                user.IsAdmin = await userManager.IsInRoleAsync(user, "Admin") ? true : false;

                CCSUsers.Add(user);
            }
        }

        public async Task<IActionResult> OnPostToggleRoleAsync(string id)
        {
            IdentityResult result;
            if (!string.IsNullOrEmpty(id))
            {
                CCSUser user = await userManager.FindByIdAsync(id);

                if (user != null)
                {
                    if (await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, "Admin");
                    }
                    else
                    {
                        result = await userManager.AddToRoleAsync(user, "Admin");
                    }

                    if (result.Succeeded)
                    {
                        return RedirectToPage("./Index");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "ERROR: Valid ID is required");
            }

            return Page();
        }
    }
}
