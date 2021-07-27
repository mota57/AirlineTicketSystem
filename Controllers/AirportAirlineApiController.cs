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
    public class AirportAirlineApiController : ControllerBase
    {

        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;


        public AirportAirlineApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAirlinesToSelect/{airportid:int}")]
        public async Task<IEnumerable<AirlineDTO>> GetAirlinesToSelect(int airportid)
        {
            var airlinesIds =  await _context.AirlineAirport
                .Where(aa => aa.AirportId == airportid)
                .Select(p => p.AirlineId)
                .ToListAsync();

            var airlinesToSelect=  await _context.Airlines
                .Where(airline => !airlinesIds.Contains(airline.Id))
                .Select(p => new AirlineDTO { Id = p.Id, Name = p.Name})
                .ToListAsync();

            return airlinesToSelect;
        }


        [HttpGet()]
        public async Task<ActionResult<AirlineAirportDTO>> Get([FromQuery] int airportid, [FromQuery] int airlineid)
        {
            var record =  await _context.AirlineAirport
                .AsNoTracking()
                .Where(p => p.AirportId == airportid && p.AirlineId == airlineid)
                .Select(p => new AirlineAirportDTO {
                        AirportId = p.Airport.Id,
                        AirportName = p.Airport.Name,
                        AirlineName = p.Airline.Name
                    }
                ).FirstOrDefaultAsync();

            if(record == null) return NotFound();

            return (record);
        }


        [HttpGet("GetAirlinesByAirportId/{airportid}")]
        public async Task<IEnumerable<AirlineDTO>> GetAirlinesByAirportId(int airportid)
        {
            var records = await _context.AirlineAirport
                .Include(p => p.Airline)
                .Where(p => p.AirportId == airportid)
                .Select(p => p.Airline)
                .ToListAsync();

            return _mapper.Map<List<AirlineDTO>>(records);
        }


        [HttpPost()]
        public async Task<ActionResult<IEnumerable<AirlineDTO>>> Post(AirlineAirportDTO dto)
        {
            var existsAirport = _context.Airports.Any(p => p.Id == dto.AirportId);
            var existsAirline = _context.Airlines.Any(p => p.Id == dto.AirlineId);

            if(!existsAirline || ! existsAirport)
                return NotFound();

            if(!AirlineAirport.ValidarDuplicadoDeRegistro(_context, dto.AirlineId, dto.AirportId))
            {
                ModelState.AddModelError("Error", "Ya este registro existe");
                return BadRequest(ModelState);
            }

            var record = _mapper.Map<AirlineAirport>(dto);
            record.Airline = null;
            record.Airport = null;

            await _context.AirlineAirport.AddAsync(record);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Post), new { dto.AirlineId, dto.AirportId }, record);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] AirlineAirportDTO dto)
        {
            var record = await _context.AirlineAirport.FirstOrDefaultAsync(p => p.Id == id);

            if (record == null)
                return NotFound();
            
            record.AirlineId = dto.AirlineId;
            _context.Update(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete()]
        public async Task<ActionResult> Delete([FromQuery] int airportid, [FromQuery] int airlineid)
        {
            var record = await _context.AirlineAirport.FirstOrDefaultAsync(p => p.AirlineId == airlineid && p.AirportId == airportid);
            if (record == null)
                return NotFound();
            _context.SetIsDelete(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
