using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireLineTicketSystem.Infraestructure.Model
{
    public class AirportDTO
    {
        public int Id { get; set; }
        public bool IsActive { get;set;}
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public List<TerminalDTO> Terminals { get;set;}
        public List<AirlineDTO> Airlines { get; set;}
    }

    public class AirlineAirportDTO
    {
        public int Id { get;set;}
        public int? AirportId { get;set;}
        public string AirlineName { get;set;}
        public string AirportName { get; set; }
        public int? AirlineId { get;set; }
        public bool IsActive { get;set;}
    }

    public class AirportCreateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string Code { get; set; }
    }


    public class TerminalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AirlineName { get;set;}
        public int AirportId { get; set; }
        public int? AirlineId { get; set; }
        public bool IsActive { get;set;}
    }

    public class AirlineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get;set;}
    }

    public class CountryDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get;set;}
    }

    public class AirplaneDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Code { get; set; }
        public int TotalSeats { get; set; }
        public int AirlineId { get; set; }
        public string AirlineName { get;set;}
    }

    public class GateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AirportId { get; set; }
        public bool IsActive { get; set; }
    }

    public class BagPriceMasterDTO
    {
        public int Id { get; set; }
        public int AirlineId { get; set; }
        public decimal PercentOfIncreaseAfterMaxPound { get; set; } //percentage
        public List<BagPriceDetailDTO> Details { get;set;}
    }

    public class BagPriceDetailDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal PoundStart { get; set; }
        public decimal? PoundEnd { get; set; }
        public int BagPriceMasterId { get; set; }
        public bool IsActive { get; set; }
    }


}
