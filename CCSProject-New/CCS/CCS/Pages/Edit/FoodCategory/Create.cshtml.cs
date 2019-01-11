using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CCS.Data;
using CCS.Models;

namespace CCS.Pages.Edit.FoodCategory
{
    public class CreateModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public CreateModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Types = Enum.GetValues(typeof(FoodCategoryType))
                .Cast<FoodCategoryType>()
                .Where(f => f != FoodCategoryType.Pantry).ToList();

            return Page();
        }

        [BindProperty]
        public CCS.Models.FoodCategory FoodCategory { get; set; }
        public IEnumerable<FoodCategoryType> Types { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Types = Enum.GetValues(typeof(FoodCategoryType))
                .Cast<FoodCategoryType>()
                .Where(f => f != FoodCategoryType.Pantry).ToList();

                return Page();
            }

            _context.FoodCategories.Add(FoodCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}