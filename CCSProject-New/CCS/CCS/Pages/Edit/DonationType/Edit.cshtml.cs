using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.DonationType
{
    public class EditModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public EditModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CCS.Models.DonationType DonationType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DonationType = await _context.DonationTypes.FirstOrDefaultAsync(m => m.DonationTypeID == id);

            if (DonationType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DonationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationTypeExists(DonationType.DonationTypeID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DonationTypeExists(int id)
        {
            return _context.DonationTypes.Any(e => e.DonationTypeID == id);
        }
    }
}
