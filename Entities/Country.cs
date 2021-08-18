using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AireLineTicketSystem.Entities
{
    public class Country
    {
        public int Id { get;set;}
        [Required, StringLength(50, MinimumLength = 2)]
        public string Code { get;set;}
        [Required, StringLength(50, MinimumLength = 2)]
        public string Name { get;set; }

        public List<Airport> Airports = new List<Airport>();
    }

    //public class TicketPrice
    //{
    //    public int Id { get;set;}
    //    public DateTime? StartTimePrice { get;set;}
    //    public DateTime? EndTimePrice { get;set; }
    //    public decimal Price { get;set;}
    //    public bool IsActive { get;set;} = true;
    //    public int TicketPriceId { get;set;}
    //    public Ticket Ticket { get; set;}
    //}
}
