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
    public class GroceryRescueModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GroceryRescueModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GroceryRescueReport GroceryReport { get; set; }
        public IEnumerable<GroceryQueryModel> Results { get; set; }

        public IActionResult OnGet()
        {

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {

            if (GroceryReport.StartDate > GroceryReport.EndDate)
            {
                ModelState.AddModelError("", "DATE ERROR: End date must be greater than or equal to the start date.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string startDate = GroceryReport.StartDate.ToString();
            string endDate = GroceryReport.EndDate.ToString();

            Results = await _context.GroceryQuery
                .FromSql(@"SELECT Agency , ISNULL([Bakery], 0.0) AS Bakery, ISNULL([Dairy], 0.0) AS Dairy, ISNULL([Deli], 0.0) AS Deli, 
	                            ISNULL([Dry Grocery], 0.0) AS DryGrocery, ISNULL([Frozen (Non-Meat)], 0.0) AS Frozen, ISNULL([Meat], 0.0) AS Meat,
	                            ISNULL([Non-Food], 0.0) AS NonFood, ISNULL([Produce], 0.0) AS Produce
                            FROM
	                            (SELECT CONCAT(a.AgencyName, ' - ', ad.StreetAddress1, ' ', ad.CityName, ', ', ad.StateShortName, ' ', ad.Zipcode) AS Agency, 
	                            x.Description, SUM(x.total_weight) AS Weight
	                            FROM Agencies a
	                            JOIN Addresses ad
		                            ON ad.AddressID = a.AddressID
	                            JOIN DonationTransactions t
		                            ON t.AgencyID = a.AgencyID
	                            JOIN (SELECT f.Description, l.DonationTransactionID, SUM(l.weight) AS total_weight
			                            FROM TransactionLineItems l 
			                            JOIN FoodCategories f
				                            ON f.FoodCategoryID = l.FoodCategoryID
				                            AND f.FoodCategoryType = 2
			                            GROUP BY f.Description, l.DonationTransactionID) x ON x.DonationTransactionID = t.DonationTransactionID
                                WHERE t.TimeStamp >= {0} AND t.TimeStamp < {1}
	                            GROUP BY a.AgencyName, ad.StreetAddress1, ad.CityName, ad.StateShortName, ad.Zipcode, x.Description) AS SourceTable
                            PIVOT(
	                            SUM(Weight)
	                            FOR Description IN ([Bakery], [Dairy], [Deli], [Dry Grocery], [Frozen (Non-Meat)], [Meat], [Non-Food], [Produce])
                            ) AS PivotTable;", startDate, endDate)
                .AsNoTracking()
                .ToListAsync();

            return Page();
        }
    }
    
}