using System;
namespace AireLineTicketSystem.Entities
{
    public class FlightPrice
    {
        public int Id { get; set; }
        public DateTime? StartTimePrice { get; set; }
        public DateTime? EndTimePrice { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int FlightScaleId { get; set; }
        public FlightScale FlightScale { get; set; }
    }
}
