using System;
using System.Linq;
using System.Threading.Tasks;
using LEPS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenDataShare;

namespace LEPS.Pages.Players
{
    public class Create : PageModel
    {
        
        private readonly ApplicationDbContext _dbContext;

        public Create(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostCreatePlayer()
        {
            var email = Request.Form["email"].ToString();
            var emailDupChk = _dbContext.Players.SingleOrDefault(p => p.Email == email);
            if (emailDupChk != default)
            {
                return Page();
            }

            var newPlayer = new Player
            {
                Email = Request.Form["email"],
                FirstName = Request.Form["firstName"],
                LastName = Request.Form["lastName"],
                CreateDate = DateTime.UtcNow
            };

            _dbContext.Add(newPlayer);
            _dbContext.SaveChanges();
            
            return RedirectToPage("./Edit", new { id = newPlayer.Id });
        }
    }
}