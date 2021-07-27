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
                .ReverseMap();

            CreateMap<Airline, AirlineDTO>()
                .ReverseMap();

            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Airplane, AirplaneDTO>()
                .ForMember(p => p.AirlineName, (p) => p.MapFrom(x => x.Airline.Name))
                .ReverseMap();
            CreateMap<Gate, GateDTO>().ReverseMap();

            CreateMap<AirlineAirport, AirlineAirportDTO>()
                 .ForMember(p => p.AirlineId, (p) => p.MapFrom(x => x.AirlineId))
                 .ForMember(p => p.AirportId, (p) => p.MapFrom(x => x.AirportId))
                .ReverseMap();

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
