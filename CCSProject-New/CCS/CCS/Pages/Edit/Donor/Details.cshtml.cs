using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.Donor
{
    public class DetailsModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public DetailsModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Agency Agency { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Agency = await _context.Agencies
                .Include(a => a.Address).FirstOrDefaultAsync(m => m.AgencyID == id);

            if (Agency == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
