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

        [HttpPost("GetAvailableFlights")]
        public async Task<List<FlightAvailableDTO>> GetAvailableFlights([FromBody] FlightAvailableParams query)
        {

            var flightIds = await _context.FlightScales
                    .Where(p => 
                        p.Order == 1 
                        && p.DepartTime.Date.Year == query.DateDeparture.Date.Year
                        && p.DepartTime.Date.Month == query.DateDeparture.Date.Month
                        && p.DepartTime.Date.Day == query.DateDeparture.Date.Day
                        && p.AirportDepartureId == query.AirportDepartureId )
                    .Select(f => f.FlightId)
                    .Distinct()
                    .ToListAsync();

            Func<DateTime,DateTime,string> buildTotalTimeStr = (d1,d2) =>
            {
                var totalTimeSpan = (d2-d1);
                return $"{totalTimeSpan.TotalHours} h  {totalTimeSpan.TotalMinutes} min";
            };

            var result  = _context.Flights
                .Include(p =>p.FlightScales).ThenInclude(p =>p.AirportDeparture).ThenInclude(p => p.Country)
                .Include(p =>p.FlightScales).ThenInclude(p =>p.Airline)
                .Include(p =>p.FlightScales).ThenInclude(p =>p.AirportArrival).ThenInclude(p => p.Country)
                .Where(f => flightIds.Contains(f.Id))
                .ToList()
                .Select(p =>
                {
                    var origin = p.FlightScales.First();
                    var destiny = p.FlightScales.Last();
                    var result = new FlightAvailableDTO
                    {
                        FlightId = p.Id,
                        Price = origin.MinPrice,
                        DateTimeStart = origin.DepartTime.ToLongDateString(),
                        DateTimeEnd = destiny.ArrivalTime.ToLongDateString(),
                        AirlineName = origin.Airline.Name,
                        CountryOrigin = origin.AirportDeparture.Country.Name,
                        CountryDestiny = destiny.AirportArrival.Country.Name,
                        TotalScales = p.FlightScales.Count(),
                        TotalTime = buildTotalTimeStr(origin.DepartTime, destiny.ArrivalTime),
                        ScalesInfo = p.FlightScales.Select(fs => 
                            new ScaleDTO { 
                                AirportDestiny = fs.AirportArrival.Name,
                                TotalTime = buildTotalTimeStr(fs.DepartTime, fs.ArrivalTime)
                            }
                       ).ToList()

                    };
                    return result;
                }).ToList();
                
            return result;

        }


        public class FlightAvailableParams
        {
            public int AirportDepartureId { get;set;}
            public int AirportArrivalId { get;set;}
            public DateTime DateDeparture { get;set;}
        }

     

    }
}
