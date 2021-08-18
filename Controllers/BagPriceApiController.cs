using AireLineTicketSystem.Infraestructure.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AireLineTicketSystem.Entities;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AireLineTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagPriceApiController : ControllerBase
    {

        private readonly AireLineTicketSystemContext _context;
        private readonly IMapper _mapper;


        public BagPriceApiController(AireLineTicketSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<BagPriceMasterDTO>> Get(int airlineId)
        {
            var result = await _context
                .BagPriceMasters
                .Include(p => p.BagPriceDetails)
                .FirstOrDefaultAsync(p => p.AirlineId == airlineId);

            //await _context.Entry(result).Collection(p => p.BagPriceDetails).LoadAsync();
            
            return _mapper.Map<BagPriceMasterDTO>(result);
        }

        [HttpPost]
        public async Task<ActionResult<BagPriceMasterDTO>> Post(BagPriceMasterDTO dto)
        {
            var recordMapped = _mapper.Map<BagPriceMaster>(dto);

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
        public async Task<ActionResult> Put(int id, [FromBody] BagPriceMasterDTO dto)
        {
            var record = await _context.BagPriceMasters.FirstOrDefaultAsync(p => p.Id == id);
            if (record == null) return NotFound();
            if (TryValidateModel(record))
            {
                //_context.SetIsModifiedFalse(record, "IsDeleted", "BagPriceMasterId", "IsActive");
                //_context.Entry(record).CurrentValues.SetValues(mapped);
                record.PercentOfIncreaseAfterMaxPound = dto.PercentOfIncreaseAfterMaxPound;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _context.BagPriceMasters.Include(b => b.BagPriceDetails).FirstOrDefaultAsync(p => p.Id == id);
            if (record == null) return NotFound();
            record.AirlineAssociated = record.AirlineId.Value;
            record.AirlineId = null;
            _context.SetIsDelete(record);
            _context.SetIsDelete(record.BagPriceDetails.Cast<Entity>().ToList());
            await _context.SaveChangesAsync();
            return NoContent();
        }



        //[HttpPost("AddOrUpdateBagPrice")]
        //public async Task<ActionResult> AddOrUpdateBagPrice(BagPriceMasterDTO dto)
        //{

        //    var recordDb = dto.Id == 0 ? null : _context.BagPriceMasters.Include(p => p.BagPriceDetails).FirstOrDefault(x => x.Id == dto.Id);
        //    var recordMapped = _mapper.Map<BagPriceMaster>(dto);

        //    this.TryValidateModel(recordMapped);

        //    if (recordDb == null)
        //    {
        //        _context.Add(recordMapped);
        //    }
        //    else
        //    {
        //        _context.Entry(recordDb).CurrentValues.SetValues(recordMapped);
        //
        //        foreach (var detail in recordMapped.BagPriceDetails)
        //        {
        //            var existingDetailRecordDb = recordDb.BagPriceDetails.FirstOrDefault(p => p.Id == detail.Id);
        //            if (existingDetailRecordDb == null)
        //            {
        //                _context.Add(detail);
        //            }
        //            else
        //            {
        //                _context.Entry(existingDetailRecordDb).CurrentValues.SetValues(detail);
        //                _context.Entry(existingDetailRecordDb).Property(p => p.BagPriceMasterId).IsModified = false;
        //            }
        //        }
        //        //TODO PENDING DELETE
        //        foreach (var detailDb in recordDb.BagPriceDetails)
        //        {
        //            if (!recordMapped.BagPriceDetails.Any(p => p.Id == detailDb.Id))
        //            {
        //                detailDb.IsDeleted = true;
        //            }
        //        }
        //    }
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
    }
}
