using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Reports
{
    public class InventoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InventoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IList<ContainerTransaction> ContainerTransaction { get;set; }
        public IList<Container> Container { get; set; }

        public async Task OnGetAsync()
        {

            ViewData["savedTransaction"] = null;

            Container = await _context.Containers
                .Include(c => c.FoodCategory)
                .ToListAsync();
            //ContainerTransaction = await _context.ContainerTransactions.ToListAsync();
            //    .Include(d => d.FoodCategory)
            //        .ThenInclude(a => a.Address)ToListAsync();
        }

        public async Task OnGetSavedAsync(int savedID)
        {
            if (savedID != null)
            {
                /*
                ViewData["savedTransaction"] = _context.ContainerTransactions
                    .Where(d => d.ContainerTransactionID == savedID).FirstOrDefault();
                    //.Include(d => d.Agency)
                    //    .ThenInclude(a => a.Address).FirstOrDefault();
                */
                ViewData["savedTransaction"] = _context.Containers
                    .Where(d => d.ContainerID == savedID).FirstOrDefault();
            }
            else
            {
                ViewData["savedTransaction"] = null;
            }

            //ContainerTransaction = await _context.ContainerTransactions.ToListAsync();
            Container = await _context.Containers
                .Include(c => c.FoodCategory)
                .ToListAsync();
            //.Include(d => d.Agency)
            //    .ThenInclude(a => a.Address).ToListAsync();
        }

        [HttpDelete]
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var container = await _context.Containers.FindAsync(id);

            if(container != null)
            {
                _context.Containers.Remove(container);
                await _context.SaveChangesAsync();
                Console.WriteLine("DELETED");
            }

            return RedirectToPage();
        }
    }
}
