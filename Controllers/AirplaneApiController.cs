﻿using AireLineTicketSystem.Entities;
using AireLineTicketSystem.Infraestructure.Model;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
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
    //[EnableCors("AireLineTicketSystemPolicyCors1")]
    [ApiController]
    public class AirplaneApiController : ControllerBase
    {
        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;


        public AirplaneApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<AirplaneApi>
        [HttpGet]
        public IEnumerable<AirplaneDTO> Get() => _mapper.Map<List<AirplaneDTO>>(_context.Airplanes.ToList());

        // GET api/<AirplaneApi>?name=faa
        [HttpGet("{name}")]
        public IEnumerable<AirplaneDTO> Get(string name)
        {
            if(string.IsNullOrEmpty(name)) return new List<AirplaneDTO>();
            return _mapper.Map<List<AirplaneDTO>>(_context.Airports.Where(p => p.Name.StartsWith(name) || p.Code.StartsWith(name)).ToList());
        }

        // GET api/<AirplaneApi>/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AirplaneDTO>> GetById(int id)
        {
            var record = await _context.Airplanes
                   .AsNoTracking()
                   .Include(x => x.Airline)
                   .FirstOrDefaultAsync(a => a.Id == id);
            if (record == null)
                return NotFound();
            return _mapper.Map<AirplaneDTO>(record);
        }

        // POST api/<AirplaneApi>
       [HttpPost]
        public async Task<ActionResult> Post([FromBody] AirplaneDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var record = _mapper.Map<Airplane>(dto);
            record.Airline =null;
            if (this.TryValidateModel(record))
            {
                await _context.Airplanes.AddAsync(record);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), new { id = record.Id }, record);
            }
            return BadRequest(ModelState);
        }


        // PUT api/<AirplaneApi>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AirplaneDTO dto)
        {
            var doesExists = await _context.Airplanes.AnyAsync(x => x.Id == id);

            if (!doesExists)
                return NotFound();

            var record = _mapper.Map<Airplane>(dto);
            record.Airline = null;
            if (this.TryValidateModel(record))
            {
                var recordDb = await _context.Airplanes.FindAsync(id);
                recordDb.Code = record.Code;
                recordDb.Brand = record.Brand;
                recordDb.Model = record.Model;
                recordDb.TotalSeats = record.TotalSeats;
                recordDb.AirlineId = record.AirlineId;
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
