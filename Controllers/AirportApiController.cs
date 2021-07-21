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

namespace AireLineTicketSystem.Controllers
{

    //en este controlador se trabajara bagMaster, BagDetail,Gate,terminal
    [Route("api/[controller]")]
    [EnableCors("AireLineTicketSystemPolicyCors1")]
    [ApiController]
    public class AirportApiController : ControllerBase
    {
        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;
        

        public AirportApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<AirportContorller>
        [HttpGet]
        public IEnumerable<AirportDTO> Get()  {
           return _mapper.Map<List<AirportDTO>>(_context.Airports.Include(p=> p.Country).ToList());
        }

        // GET api/<AirportContorller>?name=faa
        [HttpGet("{name}")]
        public IEnumerable<AirportDTO> Get(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return new List<AirportDTO>();
            }
            return _mapper.Map<List<AirportDTO>>(_context.Airports.Include(p => p.Country).Where(p => p.Name.StartsWith(name)).ToList());
        }

        // GET api/<AirportContorller>/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AirportDTO>> GetById(int id)
        {
            var record = await _context.Airports
                   .AsNoTracking()
                   .Include(c => c.Country)
                   .Include(a => a.Terminals)
                   .FirstOrDefaultAsync(a => a.Id == id);
            if(record == null)
                return NotFound();
            return _mapper.Map<AirportDTO>(record);
        }

        // POST api/<AirportContorller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AirportCreateDTO airportDTO)
        {
            if (airportDTO == null)
            {
                return BadRequest();
            }

            var airport = _mapper.Map<Airport>(airportDTO);
            airport.Country = null;

            if (this.TryValidateModel(airport))
            {
                 await _context.Airports.AddAsync(airport);
                 await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), new { id = airport.Id }, airport);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<AirportContorller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AirportDTO airportDTO)
        {
            var doesExists = await _context.Airports.AnyAsync(x => x.Id == id);

            if (!doesExists)
                return NotFound();

            var record = _mapper.Map<Airport>(airportDTO);
            record.Country = null; //do not validate country 
            if (this.TryValidateModel(record))
            {
                var recordDb = await _context.Airports.FindAsync(id);
                recordDb.CountryId = record.CountryId;
                recordDb.Name = record.Name;
                recordDb.Code= record.Code;
                recordDb.IsActive =record.IsActive;
                _context.Update(recordDb);
                await _context.SaveChangesAsync();
            } else
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        


        //public static InsertOrUpdateGraph(AirportDTO airport)
        //{
        //    var existingBlog = _context.Airports
        //    .Include(b => b.Terminals)
        //    .FirstOrDefault(b => b.BlogId == blog.BlogId);
        //    if (existingBlog == null)
        //    {
        //        context.Add(blog);
        //    }
        //    else
        //    {
        //        context.Entry(existingBlog).CurrentValues.SetValues(blog);
        //        foreach (var post in blog.Posts)
        //        {
        //            var existingPost = existingBlog.Posts
        //            .FirstOrDefault(p => p.PostId == post.PostId);
        //            if (existingPost == null)
        //            {
        //                existingBlog.Posts.Add(post);
        //            }
        //            else
        //            {
        //                context.Entry(existingPost).CurrentValues.SetValues(post);
        //            }
        //        }
        //    }
        //    context.SaveChanges();
        //}


        //// DELETE api/<AirportContorller>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var existe = await _context.Airports.AnyAsync(x => x.Id == id);

        //    if (!existe)
        //    {
        //        return NotFound();
        //    }

        //    _context.Remove(new Airport() { Id = id });
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
    }
}