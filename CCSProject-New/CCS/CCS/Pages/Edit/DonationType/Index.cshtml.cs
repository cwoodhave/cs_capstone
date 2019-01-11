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
    public class IndexModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public IndexModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CCS.Models.DonationType> DonationType { get;set; }

        public async Task OnGetAsync()
        {
            DonationType = await _context.DonationTypes.ToListAsync();
        }
    }
}
