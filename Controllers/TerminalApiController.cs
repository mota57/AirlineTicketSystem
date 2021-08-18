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


namespace AireLineTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalApiController : ControllerBase
    {

        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;


        public TerminalApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
     

        // GET api/Terminal?name=faa
        [HttpGet()]
        public IEnumerable<TerminalIndexDTO> Get(int airportId)
        {
            return _mapper.Map<List<TerminalIndexDTO>>(
                _context.Terminals
                .Include(p => p.AirlineTerminals)
                .ThenInclude(p => p.Airline)
                .Where(p => p.AirportId == airportId).ToList()
           );
        }

        // GET api/Terminal/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TerminalDTO>> GetById(int id)
        {
            var record = await _context.Terminals
                   .Include(p => p.AirlineTerminals)
                   .AsNoTracking()
                   .FirstOrDefaultAsync(a => a.Id == id);
            //avoid sending isActive = false
            record.AirlineTerminals = record.AirlineTerminals.Where(p => p.IsActive).ToList();

            if (record == null)
                return NotFound();
            return _mapper.Map<TerminalDTO>(record);
        }


        // GET api/Terminal/GetById/5
        [HttpGet("GetTerminalByParams")]
        public async Task<List<PickList>> GetTerminalByParams([FromQuery] int airlineid, [FromQuery] int airportid)
        {
            var records = await _context.AirlineTerminals
                   .Include(p => p.Terminal)
                   .AsNoTracking()
                   .Where(a => a.AirlineId == airlineid && a.AirportId == airportid && a.IsActive)
                   .Select(p => new PickList {  Id = p.TerminalId,  Name = p.Terminal.Name})
                   .ToListAsync();
            return records;
        }

        // POST api/Terminal
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TerminalDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var record = _mapper.Map<Terminal>(dto);
            record.Airport = null;
            if (this.TryValidateModel(record))
            {
                await _context.Terminals.AddAsync(record);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), null, new { record.Id });
            }
            return BadRequest(ModelState);
        }

        // PUT api/Terminal/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TerminalDTO dto)
        {
            var doesExists = await _context.Terminals.AnyAsync(x => x.Id == id);

            if (!doesExists)
                return NotFound();

            dto.Id = id;

            var recordMapped = _mapper.Map<Terminal>(dto);
            recordMapped.Airport = null;
            if (this.TryValidateModel(recordMapped))
            {
                var recordDb = await _context.Terminals.Include(p => p.AirlineTerminals).FirstOrDefaultAsync(p => p.Id == id);
                AddOrUpdateAirlineTerminal(recordDb, recordMapped);
                recordDb.Name = recordMapped.Name;
                recordDb.IsActive = recordMapped.IsActive;
                _context.Update(recordDb);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _context.Terminals.FirstOrDefaultAsync(p => p.Id == id);
            if(record == null) return NotFound();
            _context.SetIsDelete(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// Only to update the airlines that are going to be selected or deselected.
        /// </summary>
        /// <param name="recordDb"></param>
        /// <param name="recordMapped"></param>
        private void AddOrUpdateAirlineTerminal(Terminal recordDb, Terminal recordMapped)
        {

            //when user select from the list and hit save
            foreach (var airlineTerminals in recordMapped.AirlineTerminals)
            {
                var airlineGateFromDb = recordDb.AirlineTerminals.FirstOrDefault(p => p.AirlineId == airlineTerminals.AirlineId && p.TerminalId == airlineTerminals.TerminalId);
                if (airlineGateFromDb == null)
                {
                    airlineTerminals.IsActive = true;
                    _context.AirlineTerminals.Add(airlineTerminals);
                }
                else
                {
                    airlineGateFromDb.IsActive = true;
                }

            }

            //when user deselect from the list and hit save
            foreach (var airlineGate in recordDb.AirlineTerminals)
            {
                if ((recordMapped.AirlineTerminals == null || recordMapped.AirlineTerminals.Count == 0) 
                    || !recordMapped.AirlineTerminals.Any(p => p.AirlineId == airlineGate.AirlineId && p.TerminalId == airlineGate.TerminalId))
                {
                    airlineGate.IsActive = false;
                }
            }
        }
    }
}
