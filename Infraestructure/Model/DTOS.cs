using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireLineTicketSystem.Infraestructure.Model
{
    public class PickList
    {
        public int Id { get;set;}
        public string Name { get;set;}
    }

    public class SearchAirportCountryDTO
    {
        public int CountryId { get;set;}
        public string CountryAirportName { get; set; }
        public int AirportId { get;set;}
    }

    public class EntityDTO
    {
        public int Id { get;set;}
        public string Name { get;set;}
    }

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

    public class TerminalIndexDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AirlineNames { get;set;}
        public bool IsActive { get; set; }
    }

    public class TerminalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AirportId { get; set; }
        public bool IsActive { get;set;}
        public List<int> AirlinesId { get; set; }
    }


    public class AirlineIndexDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalAirplanes { get; set; }
        public bool IsActive { get; set; }
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
        public List<int> AirlinesId { get;set;}
    }



    public class GateIndexDTO
    {
        public int Id { get;set;}
        public string Name { get;set;}
        public bool IsActive { get;set;}
        public string AirlineName { get;set;}
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


    public class FlightDTO
    {
        public int Id { get;set;}
        public int PassengerId { get;set;}
        public List<FlightScaleDTO> flightScales {get;set; }
    }

    public class FlightCreateDTO
    {
        public int Id { get; set; }
        public List<FlightScaleDTO> flightScales { get; set; }
    }

    public class FlightUpdateDTO
    {
        public int Id { get; set; }
        public int PassengerId { get;set;}
    }

    public class FlightScaleDTO
    {
        public int Id { get;set; }
        public string Code { get; set; }
        public int AirportDepartureId { get; set; }

        public int AirportArrivalId { get; set; }

        public DateTime DepartTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public int? AirplaneId { get; set; }

        public int AirlineId { get; set; }

        public decimal MinPrice { get; set; }

        public int? TerminalId { get; set; }

        public int? GateId { get; set; }

        public int FlightId { get; set; }

        public decimal? TotalPaid { get; set; }
    }


    public class FlightFromToDTO
    {
        public int Id { get;set;}
        public string From { get;set;}
        public int AirportOriginId { get;set;}
        public int AirportDestinyId { get;set; }
        public string To { get;set;}
    }


    public class FlightAvailableDTO
    {
        public int FlightId { get; set; }
        public string DateTimeStart { get; set; }
        public string DateTimeEnd { get; set; }
        public string CountryOrigin { get; set; }
        public string CountryDestiny { get; set; }
        public string AirlineName { get; set; }
        public string TotalTime { get; set; }
        public int TotalScales { get; set; }
        public decimal Price { get; set; }
        public List<ScaleDTO> ScalesInfo { get; set; }
    }

    public class ScaleDTO
    {

        public string TotalTime { get; set; }
        public string AirportDestiny { get; set; }
    }
}
