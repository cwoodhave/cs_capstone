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
    public class IndexModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public IndexModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CCS.Models.FoodCategory> FoodCategory { get;set; }

        public async Task OnGetAsync()
        {
            FoodCategory = await _context.FoodCategories.Where(f => f.FoodCategoryType == FoodCategoryType.USDA).ToListAsync();
        }
    }
}
