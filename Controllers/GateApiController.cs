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
   

        // GET api/gate?name=faa&airport
        [HttpGet()]
        public IEnumerable<GateDTO> Get(int airportId)
        {
            return _mapper.Map<List<GateDTO>>(_context.Gates
                .Where(p => p.AirportId == airportId).ToList());
        }

        // GET api/gate/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GateDTO>> GetById(int id)
        {
            var record = await _context.Gates
                   .AsNoTracking()
                   .FirstOrDefaultAsync(a => a.Id == id);
            if (record == null)
                return NotFound();
            return _mapper.Map<GateDTO>(record);
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
                return CreatedAtAction(nameof(Post), new { id = record.Id }, record);
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

            var record = _mapper.Map<Gate>(dto);
            if (this.TryValidateModel(record))
            {
                var recordDb = await _context.Gates.FindAsync(id);
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
