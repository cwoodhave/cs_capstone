using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.Donor
{
    public class CreateModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public CreateModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressID");
            return Page();
        }

        [BindProperty]
        public Agency Agency { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Agencies.Add(Agency);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}