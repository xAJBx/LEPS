using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using LEPS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenDataShare;

namespace LEPS.Pages.Series
{
    public class EditModel : PageModel
    {
        
        private readonly ApplicationDbContext _dbContext;

        public EditModel(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }
        
        
        [BindProperty]
        public EventProps Eventt { get; set; }
        
        
        
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return Page();
        }

        public void OnPostDelete()
        {
            var eventt = _dbContext.Event.Single(e => e.Id == Eventt.Id);
            eventt.IsDeleted = true;
            _dbContext.SaveChanges();

        }
        
        public void OnPost()
        {
            var seriesId = Request.Query["id"];

            var series = _dbContext.Series.SingleOrDefault(s => s.Id == Int16.Parse(seriesId));
            
            //new event
            _dbContext.Add(new Event
            {
                SeriesId = Int32.Parse(seriesId),
                Name = Eventt.Name,
                DateTime = Eventt.DateTime,
                Cost = Eventt.Cost,
                Series = series
                
            });
            _dbContext.SaveChanges();
            
            
            //update event
            
            

        }

        public class EventProps
        {
            public int Id { get; set; }
            public int SeriesId { get; set; }
            public Entities.Series Series { get; set; }
            public decimal Pot { get; set; }
            public string Name { get; set; }
            public decimal Cost { get; set; }
            public DateTime DateTime { get; set; }
        }
    }
}