using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AireLineTicketSystem.Entities
{
    public class Airplane  : Entity
    {
        public int Id { get;set;}
        [Required, StringLength(50, MinimumLength = 2)]
        public string Brand { get;set;}
        [Required, StringLength(50, MinimumLength = 2)]
        public string Model { get;set; }
        [Required, StringLength(50, MinimumLength = 2)]
        public string Code { get;set;}
        public int TotalSeats { get;set;}

        public int? AirlineId {get;set; }
        public Airline Airline { get;set;}

        public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
    }

}
