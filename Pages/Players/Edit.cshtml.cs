using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenDataShare;
using AutoMapper;
using LEPS.Entities;
using Microsoft.EntityFrameworkCore;

namespace LEPS.Pages.Players
{
    public class Edit : PageModel
    {
        
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public Edit(
            ApplicationDbContext dbContext
            ,IMapper mapper 
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [BindProperty]
        public EditPlayerRequest Player { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            
            if (id == null)
            {
                return NotFound();
            }
            var player = await _dbContext.Players
                .SingleOrDefaultAsync(m => m.Id == id);

            if (player == null)
            {
                return NotFound();
            }
            Player = _mapper.Map<EditPlayerRequest>(player);
            OriginalPlayer = player;

            
            
            return Page();
        }
        public Player OriginalPlayer { get; set; }

        public class EditPlayerRequest
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public DateTime CreateDate { get; set; }
            
        }
    }
}