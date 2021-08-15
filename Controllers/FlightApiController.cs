﻿using AireLineTicketSystem.Entities;
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
    public class FlightApiController : ControllerBase
    {
        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;


        public FlightApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FlightFromToDTO>>> Get()
        {
            var result = await _context
                .Flights
                .Select(p => new Flight
                {
                    Id = p.Id,
                    FlightScales = p.FlightScales
                   .Select(fs => new FlightScale()
                   {
                       Id = fs.Id,
                       AirportDeparture = fs.AirportDeparture,
                       AirportArrival = fs.AirportArrival
                   }).ToList()
                })
                .ToListAsync();

            List<FlightFromToDTO> dtos = new List<FlightFromToDTO>();
            foreach (var flight in result)
            {
                var dto = new FlightFromToDTO();
                dto.Id = flight.Id;

                if (flight.FlightScales?.Count() == 1)
                {
                    var scale = flight.FlightScales.FirstOrDefault();

                    //var airports = await _context.Airports.Where(a => a.Id == scale.AirportDepartureId || a.Id == scale.AirportArrivalId).ToListAsync(); ;
                    //dto.From = airports.FirstOrDefault(p => p.Id == scale.AirportDepartureId)?.Name;
                    //dto.To = airports.FirstOrDefault(p => p.Id == scale.AirportArrivalId)?.Name;
                    dto.From = scale.AirportDeparture.Name;
                    dto.To = scale.AirportArrival.Name;
                }
                else if (flight.FlightScales?.Count() > 1)
                {

                    dto.From = flight.FlightScales.FirstOrDefault().AirportDeparture.Name;
                    dto.To = flight.FlightScales.LastOrDefault().AirportArrival.Name;
                }
                else
                {
                    //do nothing
                }
                dtos.Add(dto);
            }
            return dtos;
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<FlightDTO>> GetById(int id)
        {
            var result = await _context
                .Flights
                .Include(p => p.FlightScales)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (result == null) return NotFound();

            return _mapper.Map<FlightDTO>(result);
        }

        [HttpPost]
        public async Task<ActionResult<FlightCreateDTO>> Post(FlightCreateDTO dto)
        {
            var recordMapped = _mapper.Map<Flight>(dto);

            if (TryValidateModel(recordMapped))
            {
                _context.Add(recordMapped);
                await _context.SaveChangesAsync();
                dto.Id = recordMapped.Id;
                return CreatedAtAction(nameof(Post), dto, recordMapped.Id);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FlightUpdateDTO dto)
        {
            var record = await _context.Flights.FirstOrDefaultAsync(p => p.Id == id);
            if (record == null) return NotFound();
            if (TryValidateModel(record))
            {
                //_context.SetIsModifiedFalse(record, "IsDeleted", "BagPriceMasterId", "IsActive");
                //_context.Entry(record).CurrentValues.SetValues(mapped);
                record.PassengerId = dto.PassengerId;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _context.Flights.Include(b => b.FlightScales).FirstOrDefaultAsync(p => p.Id == id);
            if (record == null) return NotFound();
            _context.SetIsDelete(record);
            _context.SetIsDelete(record.FlightScales?.Cast<Entity>().ToList());
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}