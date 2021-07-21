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
    public class AirlineApiController : ControllerBase
    {
        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;


        public AirlineApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<AirlineApi>
        [HttpGet]
        public IEnumerable<AirlineDTO> Get() => _mapper.Map<List<AirlineDTO>>(_context.Airlines.ToList());

        // GET api/<AirlineApi>?name=faa
        [HttpGet("{name}")]
        public IEnumerable<AirlineDTO> Get(string name)
        {
            return _mapper.Map<List<AirlineDTO>>(_context.Airlines.Where(p => p.Name.StartsWith(name)).ToList());
        }

        // GET api/<AirlineApi>/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AirlineDTO>> GetById(int id)
        {
            var record = await _context.Airlines
                   .AsNoTracking()
                   .FirstOrDefaultAsync(a => a.Id == id);
            if (record == null)
                return NotFound();
            return _mapper.Map<AirlineDTO>(record);
        }

        // POST api/<AirlineApi>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AirlineDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var record = _mapper.Map<Airline>(dto);
            if (this.TryValidateModel(record))
            {
                await _context.Airlines.AddAsync(record);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), new { id = record.Id }, record);
            }
            return BadRequest(ModelState);
        }


        // PUT api/<AirlineApi>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AirlineDTO dto)
        {
            var doesExists = await _context.Airlines.AnyAsync(x => x.Id == id);

            if (!doesExists)
                return NotFound();

            var record = _mapper.Map<Airline>(dto);
            if (this.TryValidateModel(record))
            {
                var recordDb = await _context.Airlines.FindAsync(id);
                recordDb.Name = record.Name;
                recordDb.isActive = record.isActive;
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
