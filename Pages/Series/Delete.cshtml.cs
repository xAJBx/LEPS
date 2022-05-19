using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenDataShare;

namespace LEPS.Pages.Series
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteModel(
            ApplicationDbContext dbContex
        )
        {
            _dbContext = dbContex;
        }
        public async Task<IActionResult>  OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<RedirectResult> OnPost(int seriesId)
        {
            var series = _dbContext.Series.Single(s => s.Id == seriesId);
            series.IsDeleted = true;
            _dbContext.SaveChanges();
            return Redirect("/Series");
        }
    }
}