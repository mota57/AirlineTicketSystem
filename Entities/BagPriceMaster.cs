using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AireLineTicketSystem.Entities
{
    public class BagPriceMaster : Entity, IValidatableObject
    {
        public int Id { get;set;}
        public int AirlineId { get;set;}
        public Airline Airline { get;set;}
        public List<BagPriceDetail> BagPriceDetails = new List<BagPriceDetail>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var localizer = (IStringLocalizer) validationContext.GetService(typeof(IStringLocalizer));
            var result = new List<ValidationResult>();
            
            if (BagPriceDetails == null || BagPriceDetails.Count == 0)
            {
                result.Add(new ValidationResult(localizer["msgBagPriceNone"]));
                return result;
            }

            var bagPricesDetails = BagPriceDetails.OrderBy(p => p.PoundStart);
            foreach (var detail in bagPricesDetails)
            {
                var didColapse = BagPriceDetails.Any(p =>
                    (
                        p.PoundEnd != null
                        && detail.PoundStart >= p.PoundStart
                        && detail.PoundEnd <= p.PoundEnd
                    )
                    || (
                      p.PoundEnd == null
                      && detail.PoundStart == p.PoundStart
                    ));
              
                if (didColapse) {
                    result.Add(new ValidationResult(localizer["msgBagPriceOverlap"], new string[] { "PoundEnd", "PoundStart" }));
                    break;
                }
            }

            return null;
        }
    }



}
