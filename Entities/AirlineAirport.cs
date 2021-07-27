using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AireLineTicketSystem.Entities
{
    public class AirlineAirport : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? AirportId { get; set; }
        public Airport Airport { get;set;}
        public int? AirlineId { get; set; }
        public Airline Airline { get;set;}
        public bool IsActive { get; set; }


        public static bool ValidarDuplicadoDeRegistro(AireLineTicketSystemContext ctx, int airlineid, int airportid)
        {
            if (ctx.AirlineAirport.Any(p => p.AirlineId == airlineid && p.AirportId == airportid && p.IsDeleted == false))
            {
              
                return false;
            }
            return true;
        }
    }
}
