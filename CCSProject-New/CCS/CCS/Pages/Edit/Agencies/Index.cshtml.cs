using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.Agencies
{
    public class IndexModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public IndexModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Agency> Agency { get;set; }
        //public IList<Address> Addresses { get; set; }

        public async Task OnGetAsync()
        {
            Agency = await _context.Agencies
                .Include(a => a.Address)
                .IgnoreQueryFilters()
                .ToListAsync();
        }
    }
}
