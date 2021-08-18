using AireLineTicketSystem.Entities;
using AireLineTicketSystem.Infraestructure.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AirlineApiController> logger;

        public AirlineApiController(AireLineTicketSystemContext context, IMapper mapper, ILogger<AirlineApiController> logger)
        {
            _context = context;
            _mapper = mapper;
            this.logger = logger;
        }
        // GET: api/<AirlineApi>
        [HttpGet]
        public IEnumerable<AirlineIndexDTO> Get() => 
            _context.Airlines
            .Select(a => new AirlineIndexDTO { 
                    Id = a.Id,
                    IsActive = a.isActive,
                    Name = a.Name,
                    TotalAirplanes = a.Airplanes.Count
                })
            .ToList();

        // GET api/<AirlineApi>?name=faa
        //[HttpGet("{name}")]
        //public IEnumerable<AirlineDTO> Get(string name)
        //{
        //    return _mapper.Map<List<AirlineDTO>>(_context.Airlines.Where(p => p.Name.StartsWith(name)).ToList());
        //}

     
    

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
                return CreatedAtAction(nameof(Post), null, new { record.Id });
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


        // DELETE api/<AirportContorller>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var record = await _context.Airlines
        //                    .Include(p => p.Terminals)
        //                    .Include(p => p.Airplanes)
        //                    .Include(p => p.AirlineAirport)
        //                    .Include(p => p.AirlineGates)
        //                    .Include(p => p.BagPriceMaster)
        //                    .ThenInclude(p => p.BagPriceDetails)
        //                    .FirstOrDefaultAsync(x => x.Id == id);

        //    if (record == null)
        //    {
        //        return NotFound();
        //    }

        //    using var transaction = _context.Database.BeginTransaction();
            
        //    try
        //    {
        //        _context.SetIsDelete(record);
        //        _context.SetIsDelete(record.AirlineAirport.Cast<Entity>().ToList());
                
        //        await _context.SaveChangesAsync();

        //        //unlink terminals and gates, you dont want to delete the terminals and gates.
        //        foreach (var entity in record.Terminals)
        //        {
        //            entity.AirlineId = null;
        //            _context.Entry(entity).Property(p => p.AirlineId).IsModified = true;
        //        }


        //        _context.SetIsDelete(record.AirlineGates?.Cast<Entity>().ToList());

        //        await _context.SaveChangesAsync();

               
        //        _context.SetIsDelete(record.Airplanes?.Cast<Entity>().ToList());
        //        _context.SetIsDelete(record.BagPriceMaster);
        //        _context.SetIsDelete(record.BagPriceMaster?.BagPriceDetails?.Cast<Entity>().ToList());

        //        await _context.SaveChangesAsync();

        //        await transaction.CommitAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        logger.LogError($"Controller: {nameof(AirlineApiController)}\nMETHOD: Delete", e.ToString());
        //        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        //    }
        //    return NoContent();
        //}

    }
}
