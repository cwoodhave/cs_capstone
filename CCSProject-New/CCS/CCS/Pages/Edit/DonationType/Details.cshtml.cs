using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.DonationType
{
    public class DetailsModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public DetailsModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
