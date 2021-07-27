using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AireLineTicketSystem.Entities
{
    public class BagPriceDetail: Entity
    {
        public int Id { get; set; }
        public decimal PoundStart { get; set; }
        public decimal? PoundEnd { get; set; }
        public int BagPriceMasterId { get; set; }
        public BagPriceMaster BagPriceMaster { get; set; }
        public bool IsActive { get;set;}
        public bool IsDeleted { get;set;}
    }



}
