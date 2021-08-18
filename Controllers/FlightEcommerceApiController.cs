using AireLineTicketSystem.Entities;
using AireLineTicketSystem.Infraestructure.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireLineTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightEcommerceApiController : ControllerBase
    {

        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;


        public FlightEcommerceApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("SearchAirportCountry/{query}")]
        public async Task<List<SearchAirportCountryDTO>> SearchAirportCountry(string query)
        {
            //parametros de busqueda
            //parametros de paginacion
            var records = await _context.Airports
                .Where(c => EF.Functions.Like(c.Name, $"{query}%") || EF.Functions.Like(c.Country.Name, $"{query}%"))
                .Take(20)
                .Select((a) =>
                    new SearchAirportCountryDTO
                    {
                        CountryId = a.CountryId,
                        AirportId = a.Id,
                        CountryAirportName = a.Country.Name + $"({a.Name})"
                    }).ToListAsync();

            return records;


            //return records.Airports;
        }

        [HttpGet("GetAvailableFlights")]
        public async Task<List<AvailableFlightsDTO>> GetAvailableFlights([FromQuery] FlightAvailableParams query)
        {

            var flightIds = await _context.FlightScales
                    .Where(p => 
                        p.Order == 1 
                        && p.DepartTime.Date == query.DateDeparture.Date 
                        && p.AirportDepartureId == query.AirportDepartureId )
                    .Select(f => f.FlightId)
                    .Distinct()
                    .ToListAsync();
            
             
            ///todo 
            return null;

        }


        public class FlightAvailableParams
        {
            public int AirportDepartureId { get;set;}
            public int AirportArrivalId { get;set;}
            public DateTime DateDeparture { get;set;}
        }

        public class AvailableFlightsDTO
        {
            public int FlightId { get;set;}
            public DateTime DateTimeStart { get; set; }
            public DateTime DateTimeEnd { get; set; }
            public string CountryOrigin { get; set; }
            public string CountryDestiny { get; set; }
            public string AirlineName { get; set; }
            public string TotalTime { get; set; }
            public int TotalScales { get; set; }
            public List<ScaleDTO> scalesInfo { get; set; }
        }

        public class ScaleDTO
        {

            public string TotalTime { get; set; }
            public string AirportDestiny { get; set; }
        }

    }
}
