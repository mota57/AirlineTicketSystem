namespace AireLineTicketSystem.Entities
{
    public class AirlineTerminal : Entity
    {
        public int Id { get; set; }
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }
        public int AirportId { get; set; }
        public Airport Airport { get; set; }
        public bool IsActive { get; set; }

    }

}
