

using System;
using System.Linq;
using System.Threading.Tasks;
using LEPS.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenDataShare;

namespace LEPS.Controllers
{
    [Route("api/event")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        
        [HttpPost]
        [Route("enrollplayer")]
        public async Task<IActionResult> PlayerEnrollment([FromBody] object json)
        {
            var jsonn = JObject.Parse(json.ToString());

            var playerId = Int32.Parse(jsonn["playerId"].ToString());
            var eventId = Int32.Parse(jsonn["eventId"].ToString());

            var playerFound = _context.Players.Any(p => p.Id == playerId);
            var eventFound = _context.Players.Any(e => e.Id == eventId);

            if (!playerFound || !eventFound)
            {
                return BadRequest($"Player Found: {playerFound}, Event Found: {eventFound}");
            }
            
            var enrollment = new EventEnrollment
            {
                PlayerId = playerId,
                EventId = eventId,
                EnrollDate = DateTime.UtcNow
            };
            _context.Add(enrollment);
            _context.SaveChangesAsync();
            
            
            return Ok($"EnrollmentId: {enrollment.Id}");
        }
    }
}