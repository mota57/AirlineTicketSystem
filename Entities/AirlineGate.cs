namespace AireLineTicketSystem.Entities
{
    public class AirlineGate : Entity
    {
        public int Id { get;set;}
        public int AirlineId { get;set;}
        public Airline Airline { get;set;}
        public int GateId { get;set;}
        public Gate Gate { get;set;}
        public int AirportId { get;set;}
        public Airport Airport { get;set;}
        public bool IsActive { get;set;}
        
    }

}
