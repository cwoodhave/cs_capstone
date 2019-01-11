using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.Agencies
{
    public class CreateModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public CreateModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Address> Addresses { get; set; }

        public IActionResult OnGet()
        {
            Addresses = _context.Addresses.ToList();
            return Page();
        }

        [BindProperty]
        public Agency Agency { get; set; }

        public async Task<IActionResult> OnPostAsync(string streetAddress1 = "", string streetAddress2 = "", string city = "", 
            string state = "", string zip = "")
        {
            if(Agency.AddressID <= 0)
            {
                //If no addressID has been selected check if all "New Address" fields have values
                if ( string.IsNullOrEmpty(streetAddress1) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(zip) )
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

            //Create new address if needed
            if(Agency.AddressID <= 0)
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


            _context.Agencies.Add(Agency);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}