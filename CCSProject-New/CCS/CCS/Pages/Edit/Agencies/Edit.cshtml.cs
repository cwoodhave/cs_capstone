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

namespace CCS.Pages.Edit.Agencies
{
    public class EditModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public EditModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Agency Agency { get; set; }
        public IEnumerable<Address> Addresses { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Agency = await _context.Agencies
                .Include(a => a.Address).IgnoreQueryFilters().FirstOrDefaultAsync(m => m.AgencyID == id);

            if (Agency == null)
            {
                return NotFound();
            }
            Addresses = _context.Addresses.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string streetAddress1 = "", string streetAddress2 = "", string city = "",
            string state = "", string zip = "")
        {
            if (Agency.AddressID <= 0)
            {
                //If no addressID has been selected check if all "New Address" fields have values
                if ( string.IsNullOrEmpty(streetAddress1)  || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(zip) )
                {
                    ModelState.AddModelError(nameof(Agency.AgencyID), "Please select an existing address or fill out Street Address 1," +
                        " City, State, and Zip for a new address.");
                }
                else
                {
                    //Remove AddressID from validation when all new address fields have been entered
                    ModelState.Remove("Agency.AddressID");
                }
            }
            if (!ModelState.IsValid)
            {
                Addresses = _context.Addresses.ToList();
                return Page();
            }
            //Create New Address if Needed
            if (Agency.AddressID <= 0)
            {
                Address address = new Address
                {
                    StreetAddress1 = streetAddress1,
                    StreetAddress2 = streetAddress2 == null ? "" : streetAddress2,
                    CityName = city,
                    StateShortName = state,
                    Zipcode = zip
                };

                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();
                Agency.AddressID = address.AddressID;
            }

            _context.Attach(Agency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyExists(Agency.AgencyID))
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

        private bool AgencyExists(int id)
        {
            return _context.Agencies.Any(e => e.AgencyID == id);
        }
    }
}
