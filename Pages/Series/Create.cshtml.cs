using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenDataShare;

namespace LEPS.Pages.Series
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

        public void OnPostCreateSeries()
        {
            var inputCount = Request.Form.Keys.Count;
            var inputKeys = Request.Form.Keys;
            var seriesName = Request.Form["seriesName"];
            var seriesDesc = Request.Form["seriesDesc"];

            var newSeries = new Entities.Series
            {
                Name = seriesName,
                Desc = seriesDesc
            };
            _dbContext.Add(newSeries);
            _dbContext.SaveChanges();

            for (var i = 0; i < (inputCount - 3)/3; i++)
            {
                var seriesEvent = new Entities.Event
                {
                    Name = Request.Form[$"eventName{i}"],
                    Cost = Convert.ToDecimal(Request.Form[$"eventCost{i}"]),
                    DateTime = Convert.ToDateTime(Request.Form[$"eventDate{i}"]),
                    Series = newSeries
                    
                };
                _dbContext.Add(seriesEvent);
                
            }
            _dbContext.SaveChanges();
        }
    }
}