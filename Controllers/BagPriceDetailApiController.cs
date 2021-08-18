using AireLineTicketSystem.Entities;
using AireLineTicketSystem.Infraestructure.Model;
using AutoMapper;
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
    public class BagPriceDetailApiController : ControllerBase
    {

        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;

        public BagPriceDetailApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet("{masterId:int}")]
        public List<BagPriceDetailDTO> Get(int masterId)
        {
            var records = _mapper.Map<List<BagPriceDetailDTO>>(_context.BagPriceDetails.Where(p => p.BagPriceMasterId == masterId).ToList());
            return records;
        }

        [HttpPost()]
        public async Task<ActionResult<BagPriceDetailDTO>> Post([FromBody] BagPriceDetailDTO dto)
        {
            var recordMapped = _mapper.Map<BagPriceDetail>(dto);

            if (TryValidateModel(recordMapped))
            {
                _context.Add(recordMapped);
                await _context.SaveChangesAsync();
                dto.Id = recordMapped.Id;
                return CreatedAtAction(nameof(Post), null, new { dto.Id });
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] BagPriceDetailDTO dto)
        {
            var record = await _context.BagPriceDetails.FirstOrDefaultAsync(p => p.Id == id);
            if (record == null) return NotFound();
            if (this.TryValidateModel(record))
            {
                //_context.SetIsModifiedFalse(record, "IsDeleted", "BagPriceMasterId", "IsActive");
                //_context.Entry(record).CurrentValues.SetValues(mapped);
                record.PoundEnd = dto.PoundEnd;
                record.PoundStart = dto.PoundStart;
                record.Price = dto.Price;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _context.BagPriceDetails.FirstOrDefaultAsync(p => p.Id == id);
            if (record == null) return NotFound();
            // _context.Remove(record);
            record.IsDeleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
