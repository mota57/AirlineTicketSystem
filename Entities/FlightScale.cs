using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AireLineTicketSystem.Entities
{
    public class FlightScale : Entity //: IValidatableObject
    {
        public int Id { get;set;}
        public string Code { get;set;}
        public int AirportDepartureId { get; set; }
        [ForeignKey("AirportDepartureId")]
        public Airport AirportDeparture { get; set; }

        public int AirportArrivalId { get; set; }
        [ForeignKey("AirportArrivalId")]
        public Airport AirportArrival { get; set; }

        public DateTime DepartTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public int AirplaneId { get; set; }
        public Airplane Airplane { get; set; }

        public int AirlineId { get; set; }
        public Airline Airline { get; set; }

        public decimal MinPrice { get; set; }

        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

        public int GateId { get; set; }
        public Gate Gate { get; set; }
        
        public decimal? TotalPaid { get;set;}

        public ICollection<FlightPrice> FlightPrices { get; set; } = new HashSet<FlightPrice>();
        public ICollection<FlightBagPayment> FlightBagPayments = new HashSet<FlightBagPayment>();

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    //validate
        //}
    }

    
}
