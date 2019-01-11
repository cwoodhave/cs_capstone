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
    public class DeleteModel : PageModel
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<CCSUser> userManager;

        public DeleteModel(RoleManager<IdentityRole> roleMgr, UserManager<CCSUser> usrMgr)
        {
            roleManager = roleMgr;
            userManager = usrMgr;
        }

        [BindProperty]
        public CCSUser CCSUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CCSUser = await userManager.FindByIdAsync(id);

            if (CCSUser == null)
            {
                return NotFound();
            }

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CCSUser = await userManager.FindByIdAsync(id);

            if (CCSUser != null)
            {
                IdentityResult result = await userManager.DeleteAsync(CCSUser);
                if (result.Succeeded)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    return NotFound();
                }
            }

            return RedirectToPage("Index");

        }
            


        
    }
}
