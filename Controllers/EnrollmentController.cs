

using System;
using System.Linq;
using System.Threading.Tasks;
using LEPS.Entities;
using LEPS.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var eventFound = _context.Event.Any(e => e.Id == eventId);

            if (!playerFound || !eventFound)
            {
                return BadRequest($"Player Found: {playerFound}, Event Found: {eventFound}");
            }
            
            var enrollment = new EventEnrollment
            {
                PlayerId = playerId,
                EventId = eventId,
                Status = EventEnrollmentStatus.AccountsReceivable,
                EnrollDate = DateTime.UtcNow
            };
            _context.Add(enrollment);
            _context.SaveChangesAsync();
            
            
            return Ok($"EnrollmentId: {enrollment.Id}");
        }
        
        [HttpPost]
        [Route("payenrollment")]
        public async Task<IActionResult> editenrollment([FromBody] object json)
        {
            var jsonn = JObject.Parse(json.ToString());

            var enrollmentId = Int32.Parse(jsonn["enrollmentId"].ToString());
            var paymentAmountEvent = Decimal.Parse(jsonn["paymentAmountEvent"].ToString());
            var paymentAmountSeries = Decimal.Parse(jsonn["paymentAmountSeries"].ToString());
           
            var enrollment = await _context.EventEnrollments.SingleOrDefaultAsync(e => e.Id == enrollmentId);
            if (enrollment == default)
            {
                return BadRequest($"No Enrollment found.");
            }
            
            var eventt = await _context.Event.SingleOrDefaultAsync(e => e.Id == enrollment.EventId);
            if (eventt == default)
            {
                return BadRequest($"Event not found.");
            }

            var series = await _context.Series.SingleOrDefaultAsync(s => s.Id == eventt.SeriesId);
            if (series == default)
            {
                return BadRequest("Series not found.");
            }
            
            
            eventt.Pot += paymentAmountEvent;
            series.Pot += paymentAmountSeries;
            

            var seriesPaymentLog = new InboundTransaction
            {
                Player = enrollment.Player,
                PlayerId = enrollment.PlayerId,
                Amount = paymentAmountSeries,
                Product = Products.Series,
                ProductId = series.Id,
                DateTime = DateTime.UtcNow
            };
            
            var eventPaymentLog = new InboundTransaction
            {
                Player = enrollment.Player,
                PlayerId = enrollment.PlayerId,
                Amount = paymentAmountEvent,
                Product = Products.Event,
                ProductId = eventt.Id,
                DateTime = DateTime.UtcNow
            };


            if (paymentAmountSeries > 0)
            {
                _context.Add(seriesPaymentLog);
            }

            if (paymentAmountEvent > 0)
            {
                _context.Add(eventPaymentLog);
            }
            
            await _context.SaveChangesAsync();
            
            var eventPaySum = _context.InboundTransactions.Where(t => (t.Product == Products.Event
                                                                        && t.ProductId == eventt.Id) ||
                                                                      (t.Product == Products.Series 
                                                                       && t.ProductId == series.Id))
                                                                    .Sum(p => p.Amount);
            var eventBalance = eventPaySum - eventt.Cost;

            if (eventBalance >= 0)
            {
                enrollment.Status = EventEnrollmentStatus.Paid;
            }
            
            
            await _context.SaveChangesAsync();
            
            
            return Ok($"EnrollmentId: {enrollment.Id}");
        }

        [HttpPost]
        [Route("positionsubmition")]
        public async Task<IActionResult> enrollmentPositionSubmition([FromBody] object json)
        {
            var jsonn = JObject.Parse(json.ToString());
            var enrollmentId = Int32.Parse(jsonn["enrollmentId"].ToString());
            var position = Int32.Parse(jsonn["standing"].ToString());
            var enrollment = await _context.EventEnrollments.SingleOrDefaultAsync(e => e.Id == enrollmentId);
            enrollment.Placement = position;
            _context.SaveChanges();
            return Ok($"EnrollmentId: {enrollment.Id}, Position: {position}");
        }

    }
}