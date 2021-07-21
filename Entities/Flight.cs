namespace AireLineTicketSystem.Entities
{

    public class Flight
    {
        public int Id { get;set;}
        public int? PassengerId { get; set; }
        public Passenger Passenger { get; set; }
    }
}
