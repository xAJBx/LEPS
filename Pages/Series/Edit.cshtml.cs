using System.Collections.Generic;
using System.Threading.Tasks;
using LEPS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LEPS.Pages.Series
{
    public class EditModel : PageModel
    {
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}