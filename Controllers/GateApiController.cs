using AireLineTicketSystem.Entities;
using AireLineTicketSystem.Infraestructure.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AireLineTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GateApiController : ControllerBase
    {
        //todo validate gate is unique

        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;


        public GateApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        const int LIMIT_AIRLINE_GATES = 3;

        /// <summary>
        /// Return a specific dto for a table
        /// </summary>
        /// <param name="airportId"></param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IEnumerable<GateIndexDTO>> Get(int airportId)
        {
            var records = await _context
                    .Gates
                    .Include(p => p.AirlineGates)
                    .ThenInclude(p => p.Airline)
                    .Where(p => p.AirportId == airportId)
                    .ToListAsync();
            
            return _mapper.Map<List<GateIndexDTO>>(records);
        }


        // GET api/gate/GetById/5 This method is use for put
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GateDTO>> GetById(int id)
        {
            var record = await _context.Gates
                    .Include(p => p.AirlineGates)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == id);

            //avoid sending isActive = false
            record.AirlineGates = record.AirlineGates.Where(p => p.IsActive).ToList();
            
            if (record == null)
                return NotFound();
            return _mapper.Map<GateDTO>(record);
        }

        /// <summary>
        /// This method is use in the create ticket 
        /// </summary>
        /// <param name="airportid"></param>
        /// <param name="airlineid"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetGatesByAirportAirline))]
        public async Task<ActionResult<List<GateDTO>>> GetGatesByAirportAirline([FromQuery] int airportid, [FromQuery] int airlineid)
        {
            var records = await _context.AirlineGates
                  .Include(p => p.Gate)
                 .AsNoTracking()
                 .Where(a => a.AirlineId == airlineid && a.AirportId == airportid && a.IsActive)
                 .Select(p => p.Gate)
                 .ToListAsync();

            return _mapper.Map<List<GateDTO>>(records);
        }

        // POST api/gate
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GateDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var record = _mapper.Map<Gate>(dto);
            if (this.TryValidateModel(record))
            {
                await _context.Gates.AddAsync(record);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), null, new { record.Id });
            }
            return BadRequest(ModelState);
        }

        // PUT api/gate/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GateDTO dto)
        {
            var doesExists = await _context.Gates.AnyAsync(x => x.Id == id);

            if (!doesExists)
                return NotFound();
            
            dto.Id = id;

            var record = _mapper.Map<Gate>(dto);
            if (this.TryValidateModel(record))
            {
                var recordDb = await _context.Gates.Include(p => p.AirlineGates).FirstOrDefaultAsync(p => p.Id == id);
                AddOrUpdateAirlineGate(recordDb, record);
                recordDb.Name = record.Name;
                recordDb.IsActive = record.IsActive;
                _context.Update(recordDb);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }


        /// <summary>
        /// Only to update the airlines that are going to be selected or deselected.
        /// </summary>
        /// <param name="recordDb"></param>
        /// <param name="recordMapped"></param>
        private void AddOrUpdateAirlineGate(Gate recordDb, Gate recordMapped)
        {

            //when user select from the list and hit save
            foreach (var airlineGate in recordMapped.AirlineGates)
            {
                var airlineGateFromDb = recordDb.AirlineGates.FirstOrDefault(p => p.AirlineId == airlineGate.AirlineId && p.GateId == airlineGate.GateId);
                if (airlineGateFromDb == null)
                {
                    airlineGate.IsActive = true;
                    _context.AirlineGates.Add(airlineGate);
                } else
                {
                    airlineGateFromDb.IsActive = true;
                }
                
            }

            //when user deselect from the list and hit save
            foreach (var airlineGate in recordDb.AirlineGates)
            {
                if ((recordMapped.AirlineGates == null || recordMapped.AirlineGates.Count == 0) 
                    || !recordMapped.AirlineGates.Any(p => p.AirlineId == airlineGate.AirlineId && p.GateId == airlineGate.GateId))
                {
                    airlineGate.IsActive = false;
                }
            }
        }
    }
}
