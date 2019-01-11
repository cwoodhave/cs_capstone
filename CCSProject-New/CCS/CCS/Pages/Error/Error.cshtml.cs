using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CCS.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public IActionResult OnGet(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                var viewName = statusCode.ToString();

                if (statusCode.Value == 404)
                {
                    
                    return RedirectToPage("NotFound");
                }
                else if (statusCode.Value == 500)
                {
                    return RedirectToPage("ServerError");
                }
            }

            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return Page();
        }
    }
}
