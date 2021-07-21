using AireLineTicketSystem.Infraestructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AireLineTicketSystem.Entities;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


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
                .AsNoTracking()
                .Include(p => p.BagPriceDetails)
                .FirstOrDefaultAsync(p => p.AirlineId == airlineId);

            return _mapper.Map<BagPriceMasterDTO>(result);
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
