﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace AireLineTicketSystem.Entities
{
    public class Gate
    {
        public int Id { get;set;}
        [Required, StringLength(3, MinimumLength =1)]
        public string Name { get;set;}
        public int AirportId { get;set;}
        public Airport Airport { get;set;}
        public bool IsActive { get;set;}
        public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
    }


}
