using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CCS.Data;
using CCS.Models;
using CCS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;


namespace CCS.Pages.Transactions
{
    public class OutgoingFoodModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OutgoingFoodModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewModel = new OutgoingFoodViewModel();
            ViewModel.Donors = _context.Agencies.Where(a => a.IsActive)
                .Include(a => a.Address)
                .OrderBy(a => a.AgencyName).ToList();
            ViewModel.DonationTypes = _context.DonationTypes.OrderBy(t => t.Type).ToList();
            ViewModel.RegularFoodCategories = _context.FoodCategories
                .Where(f => f.FoodCategoryType == FoodCategoryType.Regular).ToList();
            ViewModel.USDAFoodCategories = _context.FoodCategories
                .Where(f => f.FoodCategoryType == FoodCategoryType.USDA).ToList();
            ViewModel.GroceryFoodCategories = _context.FoodCategories
                .Where(f => f.FoodCategoryType == FoodCategoryType.Grocery)
                .OrderBy(f => f.Description).ToList();
            ViewModel.InternalFoodCategories = _context.FoodCategories
                .Where(f => f.FoodCategoryType == FoodCategoryType.Internal)
                .OrderBy(f => f.Description).ToList();
            ViewModel.PantryPack = _context.FoodCategories.FirstOrDefault(f => f.FoodCategoryType == FoodCategoryType.Pantry);

            return Page();
        }

        [BindProperty]
        public DonationTransaction DonationTransaction { get; set; }
        [BindProperty]
        public List<TransactionLineItem> LineItems { get; set; }
        public OutgoingFoodViewModel ViewModel { get; set; }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            //Validation Check
            if (DonationTransaction.AgencyID < 1)
                ModelState.AddModelError(nameof(DonationTransaction.AgencyID), "Please select valid donor");
            if (!LineItems.Any())
                ModelState.AddModelError(nameof(LineItems), "Please add at least one line item");
            if (ModelState.GetValidationState("DonationTransaction.TimeStamp") == ModelValidationState.Invalid)
                ModelState.AddModelError(nameof(DonationTransaction.TimeStamp), "Please enter a valid date");

            foreach (TransactionLineItem item in LineItems)
            {
                if (item.FoodCategoryID < 1)
                    ModelState.AddModelError("LineItem.FoodCategory", "Please enter valid Food Category");
                if (item.Weight <= 0)
                    ModelState.AddModelError("LineItem.Weight", "Please enter valid weight greater than 0 and less than " + decimal.MaxValue.ToString());
                if (item.DonationTypeID < 1)
                    ModelState.AddModelError("LineItem.DonationType", "Please enter valid Donation Type");
                if (item.Cases < 1 && DonationTransaction.FoodCategory == FoodCategoryType.USDA)
                    ModelState.AddModelError("LineItem.Cases", "Please enter number of cases (1 or more)");
                if (item.Quantity < 1 && DonationTransaction.FoodCategory == FoodCategoryType.Pantry)
                    ModelState.AddModelError("LineItem.Cases", "Please enter quantity greater than 1");
            }

            if (!ModelState.IsValid)
            {
                ViewModel = new OutgoingFoodViewModel();
                ViewModel.Donors = _context.Agencies
                    .Where(a => a.IsActive)
                    .Include(a => a.Address)
                    .OrderBy(a => a.AgencyName).ToList();
                ViewModel.DonationTypes = _context.DonationTypes.OrderBy(t => t.Type).ToList();
                ViewModel.RegularFoodCategories = _context.FoodCategories
                    .Where(f => f.FoodCategoryType == FoodCategoryType.Regular)
                    .OrderBy(f => f.Description).ToList();
                ViewModel.USDAFoodCategories = _context.FoodCategories
                    .Where(f => f.FoodCategoryType == FoodCategoryType.USDA)
                    .OrderBy(f => f.Description).ToList();
                ViewModel.GroceryFoodCategories = _context.FoodCategories
                    .Where(f => f.FoodCategoryType == FoodCategoryType.Grocery)
                    .OrderBy(f => f.Description).ToList();
                ViewModel.InternalFoodCategories = _context.FoodCategories
                    .Where(f => f.FoodCategoryType == FoodCategoryType.Internal)
                    .OrderBy(f => f.Description).ToList();
                ViewModel.PantryPack = _context.FoodCategories.FirstOrDefault(f => f.FoodCategoryType == FoodCategoryType.Pantry);

                return Page();
            }

            DonationTransaction.TransactionType = TransactionTypeInOut.Out;

            _context.DonationTransactions.Add(DonationTransaction);
            await _context.SaveChangesAsync();

            foreach (TransactionLineItem item in LineItems)
            {
                item.DonationTransactionID = DonationTransaction.DonationTransactionID;

                if (DonationTransaction.FoodCategory != FoodCategoryType.Pantry)
                {
                    item.Quantity = null;
                }
                if (DonationTransaction.FoodCategory != FoodCategoryType.USDA)
                {
                    item.Cases = null;
                }

                _context.TransactionLineItems.Add(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Reports/DonationTransactions", "Saved", new { savedID = DonationTransaction.DonationTransactionID });
        }
    }
}