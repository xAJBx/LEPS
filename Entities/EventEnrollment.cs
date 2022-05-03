namespace LEPS.Entities
{
    public class EventEnrollment
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int SeriesId { get; set; }
        public Series Series { get; set; }
    }
}