using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AireLineTicketSystem.Entities
{
    public class BagPriceDetail: Entity
    {
        public int Id { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "El campo {0} debe tener un valor minimo de {1}")]
        public decimal Price { get;set;}
        [Range(0.1, 1000, ErrorMessage = "El campo {0} debe estar comprendido entre {1} y {2}")]
        public decimal PoundStart { get; set; }
        [Range(0.1, 1000, ErrorMessage = "El campo {0} debe estar comprendido entre {1} y {2}")]
        public decimal? PoundEnd { get; set; }
        public int BagPriceMasterId { get; set; }
        [ForeignKey("BagPriceMasterId")]
        public BagPriceMaster BagPriceMaster { get; set; }
        public bool IsActive { get;set;}
    }


}
