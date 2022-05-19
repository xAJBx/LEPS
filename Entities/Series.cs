namespace LEPS.Entities
{
    public class Series
    {
        public int Id { get; set; }
        public decimal Pot { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsDeleted { get; set; }
    }
}