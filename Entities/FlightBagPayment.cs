namespace AireLineTicketSystem.Entities
{
    public class FlightBagPayment
    {
        public int Id { get;set;}
        public int FlightScaleId { get;set;}
        public FlightScale FlightScale { get;set;}
        public decimal Pounds { get;set;}
        public decimal TotalPaid { get;set;}

        public int? BagPriceDetailId { get;set;}
        public BagPriceDetail BagPriceDetail { get;set;}

        public int PassengerId { get;set;}
        public Passenger Passenger { get;set;}
    }
}
