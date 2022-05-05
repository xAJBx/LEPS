using System;
using System.Linq;
using LEPS.Entities;
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

        public void OnPostCreatePlayer()
        {
            var email = Request.Form["email"].ToString();
            var emailDupChk = _dbContext.Players.SingleOrDefault(p => p.Email == email);
            if (emailDupChk != default)
            {
                return;
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
        }
    }
}