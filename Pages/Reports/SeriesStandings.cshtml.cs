using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LEPS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OpenDataShare;

namespace LEPS.Pages.Reports
{
    [BindProperties(SupportsGet = true)]
    public class SeriesStandings : PageModel
    {
        
        public int? SeriesId { get; set; }
        public bool HasSearchCriteria => SeriesId.HasValue;
        private readonly ApplicationDbContext _context;

        public SeriesStandings(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task OnGet()
        {
            if (HasSearchCriteria)
            {
                IQueryable<EventEnrollment> query = null;
                if (SeriesId.HasValue)
                {
                    query = _context.EventEnrollments.Include(e => e.Event).Where(e => e.Event.SeriesId == SeriesId);
                }
                
                var data = query.Select(e => new SeriesStandingsItem
                {
                    PlayerFirstName = e.Player.FirstName,
                    PlayerLastName = e.Player.LastName,
                    PlayerId = e.PlayerId,
                    Placement = e.Placement
                });

                List<ProcessesedStandingsItem> processesedData = new List<ProcessesedStandingsItem>();
                foreach (var d in data)
                {
                    ProcessesedStandingsItem tmpData = null;
                    if (processesedData != null)
                    { 
                        tmpData = processesedData.SingleOrDefault(pd => pd.PlayerId == d.PlayerId);
                    }

                    if (tmpData == default)
                    {
                        processesedData.Add(new ProcessesedStandingsItem
                        {
                            PlayerId = d.PlayerId,
                            PlayerFirstName = d.PlayerFirstName,
                            PlayerLastName = d.PlayerLastName,
                            PlayerPoints = d.Placement //event placement is = to num points given for that event...lower points = higher series.
                        });
                        
                    }
                    else
                    {
                        tmpData.PlayerPoints += d.Placement;
                    }
                }

                Data = processesedData.OrderBy(x => x.PlayerPoints).ToList();
                //Data = await data.OrderBy(x => x.Event.DateTime).ToListAsync();
            }
        }
        public List<ProcessesedStandingsItem> Data { get; set; }
    }

    public class ProcessesedStandingsItem
    {
        public int PlayerId { get; set; }
        public int SeriesPlacement { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public int PlayerPoints { get; set; }
    }
    
    public class SeriesStandingsItem
    {
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public int PlayerId { get; set; }
        public int Placement { get; set; }
        public Entities.Series Series { get; set; }
        public int PlayersPoints { get; set; }
    }
}
