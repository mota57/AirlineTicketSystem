using System;

namespace AireLineTicketSystem.Entities
{
    public class FlightTracker
    {
        public int Id { get; set; }
        public int? PassengerId { get; set; }
        public int FlightId { get; set; }
        public int CountryDepartId { get;set;}
        public int CountryArrivalId { get;set; }
        public FlightTrackerStatus FlightTrackerStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public enum FlightTrackerStatus
    {
        UserCancelFlight,
        UserChangeDay,
        SystemCancelFlight,
        SystemChangeDestination
    }
}
