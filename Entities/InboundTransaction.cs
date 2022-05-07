using System;
using LEPS.Enums;

namespace LEPS.Entities
{
    public class InboundTransaction
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public decimal Amount { get; set; }
        public Products Product { get; set; }
        public int ProductId { get; set; }
        public DateTime DateTime { get; set; }
    }
}