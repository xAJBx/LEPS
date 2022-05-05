using System;

namespace LEPS.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Bankroll { get; set; }
        public DateTime CreateDate { get; set; }
    }
}