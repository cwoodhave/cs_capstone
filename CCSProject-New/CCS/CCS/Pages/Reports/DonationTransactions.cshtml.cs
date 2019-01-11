using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCS.Data;
using CCS.Models;
using CCS.Models.ViewModels.Reports;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CCS.Pages.Reports
{
    public class DonationTransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DonationTransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InOutReport InOutReport { get; set; }
        public IList<DonationTransaction> DonationTransaction { get;set; }
        public IEnumerable<Agency> Agencies { get; set; }
        public IEnumerable<Agency> Donors { get; set; }
        public IEnumerable<Agency> NonDonors { get; set; }
        public IEnumerable<Agency> ReturnAgencies { get; set; }

        public IActionResult OnGet()
        {
            Agencies = _context.Agencies.Where(a => a.IsActive).Include(a => a.Address).OrderBy(a => a.AgencyName);
            Donors = _context.Agencies.Where(a => a.IsActive && a.IsDonor ).Include(a => a.Address).OrderBy(a => a.AgencyName);
            NonDonors = _context.Agencies.Where(a => a.IsActive && !a.IsDonor).Include(a => a.Address).OrderBy(a => a.AgencyName);

            return Page();
        }

        public async Task OnGetSavedAsync(int savedID)
        {
            Agencies = _context.Agencies.Where(a => a.IsActive).Include(a => a.Address).OrderBy(a => a.AgencyName);
            Donors = _context.Agencies.Where(a => a.IsActive && a.IsDonor).Include(a => a.Address).OrderBy(a => a.AgencyName);
            NonDonors = _context.Agencies.Where(a => a.IsActive && !a.IsDonor).Include(a => a.Address).OrderBy(a => a.AgencyName);

            if (savedID > 0)
            {
                DonationTransaction = await _context.DonationTransactions
                    .Where(d => d.DonationTransactionID == savedID)
                    .Include(d => d.Agency)
                        .ThenInclude(a => a.Address)
                    .Include(d => d.TransactionLineItems)
                        .ThenInclude(l => l.FoodCategory)
                    .Include(d => d.TransactionLineItems)
                        .ThenInclude(l => l.DonationType).ToListAsync();

                ViewData["savedTransaction"] = DonationTransaction.FirstOrDefault();
            }
            else
            {
                ViewData["savedTransaction"] = null;
            }

            
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            Agencies = _context.Agencies.Where(a => a.IsActive).Include(a => a.Address).OrderBy(a => a.AgencyName);
            Donors = _context.Agencies.Where(a => a.IsActive && a.IsDonor).Include(a => a.Address).OrderBy(a => a.AgencyName);
            NonDonors = _context.Agencies.Where(a => a.IsActive && !a.IsDonor).Include(a => a.Address).OrderBy(a => a.AgencyName);

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
                .OrderBy(a => a.AgencyName);

            DonationTransaction = await _context.DonationTransactions
                .Where(d => d.TimeStamp >= InOutReport.StartDate && d.TimeStamp < InOutReport.EndDate &&  InOutReport.TransactionTypes.Contains(d.TransactionType)
                            && InOutReport.Agencies.Contains(d.AgencyID) && InOutReport.FoodCategories.Contains(d.FoodCategory))
                .Include(d => d.Agency)
                    .ThenInclude(a => a.Address)
                .Include(d => d.TransactionLineItems)
                    .ThenInclude(l => l.FoodCategory)
                .Include(d => d.TransactionLineItems)
                    .ThenInclude(l => l.DonationType).ToListAsync();

            return Page();
        }
    }
}
