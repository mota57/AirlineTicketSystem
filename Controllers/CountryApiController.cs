using AireLineTicketSystem.Entities;
using AireLineTicketSystem.Infraestructure.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AireLineTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryApiController : ControllerBase
    {

        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;


        public CountryApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public static List<CountryDTO>  CountryCacheList { get; set;}

        // GET api/<CountrytApiContorller>/aa
        [HttpGet()]
        public IEnumerable<CountryDTO> Get()
        {
            if (CountryCacheList == null)
            {
                CountryCacheList = _mapper.Map<List<CountryDTO>>(_context.Countries.ToList());
            }
            return CountryCacheList;
        }


        // GET api/<CountrytApiContorller>/aa
        [HttpGet("GetByName/{name}")]
        public IEnumerable<CountryDTO> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return CountryCacheList.Take(100);
            }
            return CountryCacheList.Where(p => p.Name.StartsWith(name) || p.Code.StartsWith(name));
        }

        // GET api/<CountryController>/5
        [HttpGet("GetById/{id}")]
        public CountryDTO GetById(int id) => CountryCacheList.FirstOrDefault(c => c.Id == id);


        // PUT api/<CountryController>/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(int id, [FromBody] CountryDTO dto)
        //{
        //    var doesExists = await _context.Countries.AnyAsync(x => x.Id == id);

        //    if (!doesExists)
        //        return NotFound();

        //    var airport = _mapper.Map<CountryDTO>(dto);
        //    if (this.TryValidateModel(airport))
        //    {
        //        var recordDb = await _context.Countries.FindAsync(id);
        //        recordDb.IsActive = dto.IsActive;
        //        _context.Update(recordDb);
        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}

    }
}
