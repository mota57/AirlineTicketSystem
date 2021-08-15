using System.Collections.Generic;

namespace AireLineTicketSystem.Entities
{

    public class Flight : Entity
    {
        public int Id { get;set;}
        public int? PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public ICollection<FlightScale> FlightScales { get; set; } = new HashSet<FlightScale>();

    }
}
