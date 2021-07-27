using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AireLineTicketSystem.Entities
{
    public class Airport : Entity
    {
        public int Id { get;set;}
        [Required, StringLength(50, MinimumLength = 2)]
        public string Name { get;set;}
        [Required,StringLength(10, MinimumLength = 1)]
        public string Code { get;set;}
        public int CountryId { get; set; }
        public Country Country { get;set;}
        public bool IsActive { get;set;}
        public ICollection<AirlineAirport> AirlineAirport { get; set; } = new HashSet<AirlineAirport>();
        public ICollection<Terminal> Terminals { get; set; } = new HashSet<Terminal>();
        public ICollection<Gate> Gates { get; set; } = new HashSet<Gate>();
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
