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
        public IEnumerable<TerminalDTO> Get(int airportId)
        {
            return _mapper.Map<List<TerminalDTO>>(
                _context.Terminals.Where(p => p.AirportId == airportId).ToList()
           );
        }

        // GET api/Terminal/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TerminalDTO>> GetById(int id)
        {
            var record = await _context.Terminals
                   .AsNoTracking()
                   .FirstOrDefaultAsync(a => a.Id == id);
            if (record == null)
                return NotFound();
            return _mapper.Map<TerminalDTO>(record);
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
            if (this.TryValidateModel(record))
            {
                await _context.Terminals.AddAsync(record);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), new { id = record.Id }, record);
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

            var record = _mapper.Map<Terminal>(dto);
            if (this.TryValidateModel(record))
            {
                var recordDb = await _context.Terminals.FindAsync(id);
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
    }
}
