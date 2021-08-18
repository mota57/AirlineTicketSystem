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


            TerminalMapperHelper.Build(this);

            GateMapperHelper.Build(this);

            CreateMap<Airline, AirlineDTO>()
                .ReverseMap();

            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Airplane, AirplaneDTO>()
                .ForMember(p => p.AirlineName, (p) => p.MapFrom(x => x.Airline.Name))
                .ReverseMap();



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

    }

    public static class GateMapperHelper
    {
        const int LIMIT_AIRLINE_NAME = 2;


        public static void Build(AirlineMapperProfile profile)
        {

            profile.CreateMap<Gate, GateDTO>()
                .ForMember(p => p.AirlinesId, x => x.MapFrom(BuildAirlinesIdFromAirlineGate));

            profile.CreateMap<GateDTO, Gate>()
                .ForMember(p => p.AirlineGates, x => x.MapFrom(BuildAirlineGateFromAirlinesId));

            profile.CreateMap<Gate, GateIndexDTO>()
                .ForMember(p => p.AirlineName, x => x.MapFrom(BuildAirlineNamesFromAirlineGate));
        }

        static string BuildAirlineNamesFromAirlineGate(Gate gate, GateIndexDTO dto)
        {
            var truncateContent = gate.AirlineGates.Count() > LIMIT_AIRLINE_NAME ? "..." : "";
            return string.Join(",",
                           gate.AirlineGates.Where(p => p.IsActive).Select(p => p.Airline)
                          .Select(p => p.Name)
                          .Take(LIMIT_AIRLINE_NAME)) + truncateContent;
        }

        static  List<int> BuildAirlinesIdFromAirlineGate(Gate gate, GateDTO dto)
        {
            var output = new List<int>();
            if (gate.AirlineGates == null || gate.AirlineGates.Count == 0) return output;
            output.AddRange(gate.AirlineGates.Select(p => p.AirlineId));
            return output;
        }

        static  List<AirlineGate> BuildAirlineGateFromAirlinesId(GateDTO dto, Gate gate)
        {
            var output = new List<AirlineGate>();
            if (dto.AirlinesId == null || dto.AirlinesId.Count == 0) return output;
            output.AddRange(dto.AirlinesId.Select(id => new AirlineGate { AirportId = dto.AirportId, AirlineId = id, GateId = dto.Id }));
            return output;
        }
    }


    public static class TerminalMapperHelper
    {
        const int LIMIT_AIRLINE_NAME = 2;

        public static void Build(AirlineMapperProfile profile)
        {
            profile.CreateMap<Terminal, TerminalIndexDTO>()
               .ForMember(p => p.AirlineNames, (p) => p.MapFrom(BuildAirlineNameFromTerminalDto));

            profile.CreateMap<TerminalDTO, Terminal>()
                  .ForMember(p => p.AirlineTerminals, x => x.MapFrom(BuildAirlineTerminalsFromAirlineIds));

            profile.CreateMap<Terminal, TerminalDTO>()
                .ForMember(p => p.AirlinesId, x => x.MapFrom(BuildAirlinesIdFromAirlineTerminal));
        }


        public static string BuildAirlineNameFromTerminalDto(Terminal terminal, TerminalIndexDTO dto)
        {
            var truncateContent = terminal.AirlineTerminals.Count() > LIMIT_AIRLINE_NAME ? "..." : "";
            return string.Join(",",
                           terminal.AirlineTerminals.Where(p => p.IsActive).Select(p => p.Airline)
                          .Select(p => p.Name)
                          .Take(LIMIT_AIRLINE_NAME)) + truncateContent;
        }

        public static List<int> BuildAirlinesIdFromAirlineTerminal(Terminal record, TerminalDTO dto)
        {
            var output = new List<int>();
            if (record.AirlineTerminals == null || record.AirlineTerminals.Count == 0) return output;
            output.AddRange(record.AirlineTerminals.Select(p => p.AirlineId));
            return output;
        }

        public static List<AirlineTerminal> BuildAirlineTerminalsFromAirlineIds(TerminalDTO dto, Terminal record)
        {
            var output = new List<AirlineTerminal>();
            if (dto.AirlinesId == null || dto.AirlinesId.Count == 0) return output;
            output.AddRange(dto.AirlinesId.Select(id => new AirlineTerminal { AirportId = dto.AirportId, AirlineId = id, TerminalId = dto.Id }));
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
