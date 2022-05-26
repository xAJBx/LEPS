using System;
using LEPS.Enums;

namespace LEPS.Entities
{
    public class EventEnrollment
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public DateTime EnrollDate { get; set; }
        public EventEnrollmentStatus Status { get; set; }
        public int Placement { get; set; }
    }
}