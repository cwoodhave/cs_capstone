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

namespace CCS.Pages.Edit.SubCategory.USDA
{
    public class EditModel : PageModel
    {
        private readonly CCS.Data.ApplicationDbContext _context;

        public EditModel(CCS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FoodCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodCategoryExists(FoodCategory.FoodCategoryID))
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

        private bool FoodCategoryExists(int id)
        {
            return _context.FoodCategories.Any(e => e.FoodCategoryID == id);
        }
    }
}
