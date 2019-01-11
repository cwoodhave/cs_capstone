using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CCS.Data;
using CCS.Models;
using CCS.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CCS.Pages.Transactions
{
    public class IncomingContainerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IncomingContainerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewModel = new IncomingContainerViewModelcs();
            ViewModel.FoodCategories = _context.FoodCategories.ToList();
            return Page();
        }
        
        [BindProperty]
        public Container Container { get; set; }
        public IncomingContainerViewModelcs ViewModel { get; set; }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            //Validation Check
            //if(ContainerTransaction.FoodCategoryID < 1)
            //    ModelState.AddModelError(nameof(DonationTransaction.AgencyID), "Please select valid food category");
            if (Container == null)
                ModelState.AddModelError(nameof(Container), "Please add at least one line item");
            if (ModelState.GetValidationState("ContainerTransaction.TimeStamp") == ModelValidationState.Invalid)
                ModelState.AddModelError(nameof(Container.DateCreated), "Please enter a valid date");

            if (!ModelState.IsValid)
            {
                ViewModel = new IncomingContainerViewModelcs();
                ViewModel.FoodCategories = _context.FoodCategories.ToList();
                return Page();
            }

            //List<FoodCategory> list = _context.FoodCategories.Where(fc => fc.FoodCategoryID == Container.FoodCategoryID).ToList();
            //Container.FoodCategory = list.FirstOrDefault();
            _context.Containers.Add(Container);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Reports/Inventory", "Saved", new { savedID = Container.ContainerID });
        }
    }
}