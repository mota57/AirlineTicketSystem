using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AireLineTicketSystem.Entities
{
    public class Airline
    {
      
        public int Id { get;set;}
        [Required, StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        public bool isActive { get;set;}
        public ICollection<AirlineAirport> AirlineAirport { get; set; } = new HashSet<AirlineAirport>();
        public ICollection<Airplane> Airplanes { get; set; } = new HashSet<Airplane>();
        public ICollection<Terminal> Terminals { get; set; } = new HashSet<Terminal>();
        public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
    }

   
}
