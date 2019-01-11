using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.SubCategory.USDA
{
    public class DetailsModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public DetailsModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public CCS.Models.FoodCategory FoodCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FoodCategory = await _context.FoodCategories.FirstOrDefaultAsync(m => m.FoodCategoryID == id);

            if (FoodCategory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
