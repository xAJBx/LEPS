using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LEPS.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenDataShare;

namespace LEPS.Pages.Reports
{
    [BindProperties(SupportsGet = true)]
    public class EventStandings : PageModel
    {
        public int? EventId { get; set; }
        public bool HasSearchCriteria => EventId.HasValue;
        private readonly ApplicationDbContext _context;

        public EventStandings(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task OnGet()
        {
            if (HasSearchCriteria)
            {
                IQueryable<EventEnrollment> query = _context.EventEnrollments;

                if (EventId.HasValue)
                {
                    query = query.Where(e => e.EventId == EventId);
                }

                var data = query.Select(e => new EventStandingsItem
                {
                    EventName = e.Event.Name,
                    PlayerFirstName = e.Player.FirstName,
                    PlayerLastName = e.Player.LastName,
                    EventEnrollmentId = e.Id,
                    PlayerId = e.PlayerId,
                    Event = e.Event,
                    Placement = e.Placement
                });
                Data = await data.OrderBy(x => x.Event.DateTime).ToListAsync();
            }
        }
        public IEnumerable<EventStandingsItem> Data { get; set; }
    }
    public class EventStandingsItem
    {
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public int EventEnrollmentId { get; set; }
        public int PlayerId { get; set; }
        public int Placement { get; set; }
        public Event Event { get; set; }
        public string EventName { get; set; }
    }
}
