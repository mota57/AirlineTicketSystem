using AireLineTicketSystem.Entities;
using AireLineTicketSystem.Infraestructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireLineTicketSystem.Infraestructure
{
    public class AirlineMapperProfile : AutoMapper.Profile
    {
        public AirlineMapperProfile()
        {
            CreateMap<Foo, FooDTO>();

            CreateMap<Airport, AirportDTO>()
            .ForMember(p => p.CountryName, (p) => p.MapFrom(x =>  x.Country.Name))       
            .ForMember(p => p.CountryCode, (p) => p.MapFrom(x => x.Country.Code))
            .ReverseMap();

            CreateMap<AirportCreateDTO, Airport>();

            CreateMap<Terminal, TerminalDTO>()
                .ForMember(p => p.AirlineName, (p) => p.MapFrom(x => x.Airline.Name));

            CreateMap<TerminalDTO, Terminal>();

            CreateMap<Airline, AirlineDTO>()
                .ReverseMap();

            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Airplane, AirplaneDTO>()
                .ForMember(p => p.AirlineName, (p) => p.MapFrom(x => x.Airline.Name))
                .ReverseMap();


            CreateMap<Gate, GateDTO>()
                .ForMember(p => p.AirlinesId, x => x.MapFrom(MapFromAirlineGatesToAirlineIds));

            CreateMap<GateDTO, Gate>()
                .ForMember(p => p.AirlineGates, x => x.MapFrom(MapFromAirlinesId));
                

            CreateMap<Gate, GateIndexDTO>()
                .ForMember(p => p.AirlineName, x => x.MapFrom(BuildAirlineNameColumn));

            CreateMap<AirlineAirport, AirlineAirportDTO>()
                 .ForMember(p => p.AirlineId, (p) => p.MapFrom(x => x.AirlineId))
                 .ForMember(p => p.AirportId, (p) => p.MapFrom(x => x.AirportId))
                 .ForMember(p => p.AirlineName, (p) => p.MapFrom(x => x.Airline.Name))
                .ReverseMap();
            
            CreateMap<BagPriceMaster, BagPriceMasterDTO>()
                .ForMember(p => p.Details, (p) => p.MapFrom(x => x.BagPriceDetails))
                .ReverseMap();

            CreateMap<BagPriceDetail, BagPriceDetailDTO>().ReverseMap();
            CreateMap<Flight, FlightDTO>().ReverseMap();
            CreateMap<Flight, FlightCreateDTO>().ReverseMap();
            CreateMap<FlightScale, FlightScaleDTO>().ReverseMap();

        }


        private string BuildAirlineNameColumn(Gate gate, GateIndexDTO dto)
        {
            var LIMIT_AIRLINE_GATES = 3;
            var truncateContent = gate.AirlineGates.Count() > LIMIT_AIRLINE_GATES ? "..." : "";
            return string.Join(",",
                           gate.AirlineGates.Select(p => p.Airline)
                          .Select(p => p.Name)
                          .Take(LIMIT_AIRLINE_GATES)) + truncateContent;
        }

        public List<int> MapFromAirlineGatesToAirlineIds(Gate gate, GateDTO dto)
        {
            var output = new List<int>();
            if (gate.AirlineGates == null || gate.AirlineGates.Count == 0) return output;
            output.AddRange(gate.AirlineGates.Select(p => p.AirlineId));
            return output;
        }

        public List<AirlineGate> MapFromAirlinesId(GateDTO dto, Gate gate)
        {
            var output = new List<AirlineGate>();
            if(dto.AirlinesId == null || dto.AirlinesId.Count == 0 ) return output;
            output.AddRange(dto.AirlinesId.Select(id => new AirlineGate { AirportId = dto.AirportId, AirlineId = id, GateId = dto.Id  }));
            return output;
        }
    }

    public class Foo
    {
        public string Name { get;set;}
    }
    public class FooDTO
    {
        public string Name { get; set; }
    }
}
