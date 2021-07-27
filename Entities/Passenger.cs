using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AireLineTicketSystem.Entities
{
    public class Passenger : Entity
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength =1)]
        public string Name { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string LastName { get; set; }
        [StringLength(30, MinimumLength = 1)]
        public string PassportCode { get; set; }
        public int CountryPassportId { get;set;}
        [ForeignKey("CountryPassportId")]
        public Country CountryPassport { get;set;}
        public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
    }

  
}
