using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.Addresses
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Address> Addresses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Addresses = await _context.Addresses
                .OrderBy(a => a.StreetAddress1)
                .ThenBy(a => a.CityName)
                .ToListAsync();

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Address address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                ModelState.AddModelError(nameof(Address.AddressID), "Please select a valid address");
            }
            else
            {
                Agency agency = _context.Agencies.FirstOrDefault(a => a.AddressID == address.AddressID);

                if (agency != null)
                {
                    ModelState.AddModelError(nameof(Address.AddressID), "Address is currently still assigned to an Agency. Please remove the address from that agency before attempting to delete.");
                }
            }

            if (!ModelState.IsValid)
            {
                Addresses = await _context.Addresses
                    .OrderBy(a => a.StreetAddress1)
                    .ThenBy(a => a.CityName)
                    .ToListAsync();

                return Page();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return RedirectToPage("./../Index");
        }
    }
}
