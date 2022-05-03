using System;

namespace LEPS.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int SeriesId { get; set; }
        public Series Series { get; set; }
        public decimal Pot { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateTime { get; set; }
    }
}