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
    public class FlightScaleApiController : ControllerBase
    {


        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;

        public FlightScaleApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{flightId:int}")]
        public List<FlightScaleDTO> Get(int flightId)
        {
            var records = _mapper.Map<List<FlightScaleDTO>>(_context.FlightScales.Where(p => p.FlightId == flightId).ToList());
            return records;
        }

        [HttpPost()]
        public async Task<ActionResult<FlightScaleDTO>> Post([FromBody] FlightScaleDTO dto)
        {
            var recordMapped = _mapper.Map<FlightScale>(dto);

            if (TryValidateModel(recordMapped) )
            {
                recordMapped.Order = GetNextOrder(recordMapped.FlightId);
                _context.Add(recordMapped);
                await _context.SaveChangesAsync();
                dto.Id = recordMapped.Id;
                return CreatedAtAction(nameof(Post), null, recordMapped.Id);
            }

            return BadRequest(ModelState);
        }

        [NonAction]
        public int GetNextOrder(int flightId)
        {
            if (_context.FlightScales.Count(p => p.FlightId == flightId) == 0) return 1;
            var order = _context.FlightScales.Where(p => p.FlightId == flightId).Max(p => p.Order);
            return order + 1;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FlightScaleDTO dto)
        {
            var record = await _context.FlightScales.FirstOrDefaultAsync(p => p.Id == id);
            if (record == null) return NotFound();
            var recordMapped = _mapper.Map<FlightScale>(dto);
            
            if (this.TryValidateModel(record) )
            {
                _context.SetIsModifiedFalse(record, "IsDeleted", "Id", "FlightId", "Order");
                _context.Entry(record).CurrentValues.SetValues(recordMapped);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //   //todo
        //}

        [NonAction]
        public bool TryValidateFlightScale(FlightScale scale) {
            var flightScales = _context.FlightScales.Where(p => p.FlightId == scale.FlightId).OrderBy(p => p.DepartTime).ToList();
            return TryValidateCanCreateFlightScaleForDate(scale) 
            && TryValidateNoDuplicates(flightScales, scale);
        }


        //AVOID OVERPASS THE LIMIT OF FLIGHTS FOR AN AIRLINE THAT HAS CERTAIN AMOUNT OF AIRPLANES IN AN SPECIFIC AIRPORT.
        [NonAction]
        public bool TryValidateCanCreateFlightScaleForDate(FlightScale scale)
        {
            var totalAirplanesForAnAirline = _context.Airplanes.Count(p => p.AirlineId == scale.AirlineId);

            var totalAirplaneReserverd = _context.FlightScales.Count(p =>
                p.DepartTime.Date == scale.DepartTime.Date
                && p.AirportDepartureId == scale.AirportDepartureId
                && p.AirlineId == scale.AirlineId
                && p.AirplaneId == scale.AirplaneId
                );

            if (totalAirplaneReserverd == totalAirplanesForAnAirline)
            {
                var nameAirline = _context.Airlines.Find(scale.AirlineId);
                var nameAirport = _context.Airports.Find(scale.AirportDepartureId);
                ModelState.AddModelError("Error", $"No puede crear mas vuelos para la fecha {scale.DepartTime.Date} de la aerolinea {nameAirline} en el aeropuerto {nameAirport}");
                return false;
            }
            return true;
        }

        [NonAction]
        public bool TryValidateNoDuplicates(List<FlightScale> flightScales, FlightScale scale)
        {
            var isCreate = scale.Id == 0;

            //AVOID DUPLICATE FLIGHT SCALE FOR THE SAME AIRPORT DEPARTURE IN A SERIES OF SCALES
            if(flightScales.Any(p =>
                p.Id != scale.Id
                && p.AirportDepartureId == scale.AirportDepartureId
            ))
            {
                var nameAirport = _context.Airports.Where(a => a.Id == scale.AirportDepartureId).Select(p => p.Name).FirstOrDefault();

                ModelState.AddModelError("Error", $"No puede crear vuelos duplicados para el aeropuerto de origen '{nameAirport}'");
                return false;
            }

            //AVOID DUPLICATE FLIGHT SCALE FOR THE SAME AIRPORT ARRIVAL IN A SERIES OF SCALES
            if (flightScales.Any(p =>
                 p.Id != scale.Id
                 && p.AirportArrivalId == scale.AirportArrivalId
            ))
            {
                var nameAirport = _context.Airports.Where(a => a.Id == scale.AirportArrivalId).Select(p => p.Name).FirstOrDefault();
                ModelState.AddModelError("Error", $"No puede crear vuelos duplicados para el aeropuerto de destino '{nameAirport}'");
                return false;
            }

            int TOTAL_TIME_TO_WAIT_FOR_NEXT_FLIGHT = 2; //in minutes
            
            if (isCreate && !flightScales.All(p =>
                 scale.DepartTime > p.DepartTime.AddHours(TOTAL_TIME_TO_WAIT_FOR_NEXT_FLIGHT)
                 && scale.ArrivalTime > p.ArrivalTime.AddHours(TOTAL_TIME_TO_WAIT_FOR_NEXT_FLIGHT)
            ))
            {
                ModelState.AddModelError("Error", $"No puede crear una escala en una fecha menor a las anteriores, debe de tener como minimo {TOTAL_TIME_TO_WAIT_FOR_NEXT_FLIGHT} horas mayores de diferencia.");
                return false;
            } else
            {
                if(flightScales.Count > 1)
                {

                    var prevFlight = flightScales.FirstOrDefault(p => p.Order == scale.Order - 1);
                    var nextFlight = flightScales.FirstOrDefault(p => p.Order == scale.Order + 1);
                    if (    scale.DepartTime < prevFlight.DepartTime
                        ||  scale.DepartTime < prevFlight.ArrivalTime
                        ||  scale.ArrivalTime < prevFlight.DepartTime
                        ||  scale.ArrivalTime < prevFlight.ArrivalTime )
                    {
                        ModelState.AddModelError("Error", "No puede declarar una fecha menor a el vuelo anterior");
                        return false;
                    }

                    if (nextFlight !=null && (scale.DepartTime > nextFlight.DepartTime
                        || scale.DepartTime > nextFlight.ArrivalTime
                        || scale.ArrivalTime > nextFlight.DepartTime
                        || scale.ArrivalTime > nextFlight.ArrivalTime))
                    {

                        ModelState.AddModelError("Error", "No puede declarar una fecha mayor al vuelo posterior");
                        return false;
                    }
                }
            }

            return true;
        }

     
    }
}
