using System.ComponentModel.DataAnnotations.Schema;

namespace AireLineTicketSystem.Entities
{
    public class AirlineAirport
    {
        public int AirportsId { get; set; }
        [ForeignKey("AirportsId")]
        public Airport Airport { get;set;}

        public int AirlinesId { get; set; }
        [ForeignKey("AirlinesId")]
        public Airline Airline { get;set;}

        public bool IsActive { get; set; }
    }
}
