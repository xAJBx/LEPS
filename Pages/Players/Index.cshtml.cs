using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LEPS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenDataShare;

namespace LEPS.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Player> Player { get; set; }

        [BindProperty(SupportsGet = true)]
        public TalentIndexSearchTerms Search { get; set; }

        
        public void OnGet()
        {
            
        }
        
        public async Task OnGetSearchAsync()
        {
            IQueryable<Player> player = _context.Players;

            if (!Search.HasSearchTerms)
            {
                ModelState.AddModelError(string.Empty, "Please enter search terms.");
                return;
            }
            
            
            if (!string.IsNullOrWhiteSpace(Search.FirstName))
            {
                player = player.Where(r => r.FirstName.Contains(Search.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(Search.LastName))
            {
                player = player.Where(r => r.LastName.Contains(Search.LastName));
            }

            

            Player = await player.ToListAsync();
        }
        
        public class TalentIndexSearchTerms
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            
            public bool HasSearchTerms => !string.IsNullOrWhiteSpace(FirstName) || !string.IsNullOrWhiteSpace(LastName); 
        }
    }
}