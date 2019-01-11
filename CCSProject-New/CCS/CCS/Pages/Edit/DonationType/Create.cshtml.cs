using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.DonationType
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
            return Page();
        }

        [BindProperty]
        public CCS.Models.DonationType DonationType { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DonationTypes.Add(DonationType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}