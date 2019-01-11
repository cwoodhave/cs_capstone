using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCS.Data;
using CCS.Models;
using CCS.Models.ViewModels.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CCS.Pages.Reports
{
    public class DonationsByAgencyModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DonationsByAgencyModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LineItemsReport InOutReport { get; set; }
        public IList<TransactionLineItem> LineItems { get; set; }
        public IEnumerable<Agency> Agencies { get; set; }
        public IEnumerable<Agency> ReturnAgencies { get; set; }
        public IEnumerable<Agency> Donors { get; set; }
        public IEnumerable<Agency> NonDonors { get; set; }
        public IEnumerable<FoodCategory> AllCategories { get; set; }
        public IEnumerable<FoodCategory> ReturnCategories { get; set; }
        public IEnumerable<FoodCategory> Regular { get; set; }
        public IEnumerable<FoodCategory> USDA { get; set; }
        public IEnumerable<FoodCategory> Grocery { get; set; }
        public IEnumerable<FoodCategory> Internal { get; set; }
        public FoodCategory PantryPack { get; set; }

        public IActionResult OnGet()
        {
            Agencies = _context.Agencies.Where(a => a.IsActive).Include(a => a.Address).OrderBy(a => a.AgencyName);
            Donors = _context.Agencies.Where(a => a.IsActive && a.IsDonor).Include(a => a.Address).OrderBy(a => a.AgencyName);
            NonDonors = _context.Agencies.Where(a => a.IsActive && !a.IsDonor).Include(a => a.Address).OrderBy(a => a.AgencyName);
            AllCategories = _context.FoodCategories.OrderBy(f => f.Description);
            Regular = _context.FoodCategories.Where(f => f.FoodCategoryType == FoodCategoryType.Regular).OrderBy(f => f.Description);
            USDA = _context.FoodCategories.Where(f => f.FoodCategoryType == FoodCategoryType.USDA).OrderBy(f => f.Description);
            Grocery = _context.FoodCategories.Where(f => f.FoodCategoryType == FoodCategoryType.Grocery).OrderBy(f => f.Description);
            Internal = _context.FoodCategories.Where(f => f.FoodCategoryType == FoodCategoryType.Internal).OrderBy(f => f.Description);
            PantryPack = _context.FoodCategories.FirstOrDefault(f => f.FoodCategoryType == FoodCategoryType.Pantry);

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            Agencies = _context.Agencies.Where(a => a.IsActive).Include(a => a.Address).OrderBy(a => a.AgencyName);
            Donors = _context.Agencies.Where(a => a.IsActive && a.IsDonor).Include(a => a.Address).OrderBy(a => a.AgencyName);
            NonDonors = _context.Agencies.Where(a => a.IsActive && !a.IsDonor).Include(a => a.Address).OrderBy(a => a.AgencyName);
            AllCategories = _context.FoodCategories.OrderBy(f => f.Description);
            Regular = _context.FoodCategories.Where(f => f.FoodCategoryType == FoodCategoryType.Regular).OrderBy(f => f.Description);
            USDA = _context.FoodCategories.Where(f => f.FoodCategoryType == FoodCategoryType.USDA).OrderBy(f => f.Description);
            Grocery = _context.FoodCategories.Where(f => f.FoodCategoryType == FoodCategoryType.Grocery).OrderBy(f => f.Description);
            Internal = _context.FoodCategories.Where(f => f.FoodCategoryType == FoodCategoryType.Internal).OrderBy(f => f.Description);
            PantryPack = _context.FoodCategories.FirstOrDefault(f => f.FoodCategoryType == FoodCategoryType.Pantry);

            if (InOutReport.StartDate > InOutReport.EndDate)
            {
                ModelState.AddModelError("", "DATE ERROR: End date must be greater than or equal to the start date.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            ReturnAgencies = _context.Agencies
                .Where(a => a.IsActive && InOutReport.Agencies.Contains(a.AgencyID))
                .Include(a => a.Address)
                .OrderBy(a => a.AgencyName).ToList();

            ReturnCategories = _context.FoodCategories
                .Where(f => InOutReport.FoodCategories.Contains(f.FoodCategoryID)).OrderBy(f => f.Description).ToList();

            var filteredDonTrans = _context.DonationTransactions
                .Where(t => t.TimeStamp >= InOutReport.StartDate && t.TimeStamp < InOutReport.EndDate && InOutReport.Agencies.Contains(t.AgencyID)
                            && InOutReport.TransactionTypes.Contains(t.TransactionType)).ToList();

            var filteredDonTransIds = filteredDonTrans.Select(d => d.DonationTransactionID).ToList();

            LineItems = _context.TransactionLineItems
                .Where(l => filteredDonTransIds.Contains(l.DonationTransactionID) && InOutReport.FoodCategories.Contains(l.FoodCategoryID))
                .Include(l => l.DonationType)
                .Include(l => l.FoodCategory)
                .Include(l => l.DonationTransaction)
                    .ThenInclude(t => t.Agency)
                        .ThenInclude(a => a.Address)
                .OrderBy(l => l.DonationTransaction.AgencyID)
                .ThenByDescending(l => l.DonationTransaction.TimeStamp)
                .ThenBy(l => l.FoodCategory.FoodCategoryType)
                .ThenBy(l => l.FoodCategory.Description)
                .AsNoTracking()
                .ToList();

            return Page();
        }
    }
}