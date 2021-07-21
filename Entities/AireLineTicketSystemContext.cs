using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace AireLineTicketSystem.Entities
{

    public class AireLineTicketSystemContext :  DbContext
    {

        public AireLineTicketSystemContext(DbContextOptions<AireLineTicketSystemContext> options)
            :base(options)
        {
           
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Airline> Airlines { get; set; }

        public DbSet<BagPriceMaster> BagPriceMasters { get; set; }
        public DbSet<BagPriceDetail> BagPriceDetails { get; set; }

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightPrice> FlightPrices { get; set; }
        public DbSet<AirlineAirport> AirlineAirport { get; set; }
        public DbSet<FlightBagPayment> FlightBagPayment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            RestrictAll(modelBuilder);
            SetCountrySeedData(modelBuilder);
            SetAirLineAirportConfiguration(modelBuilder);
            SetConvetionToGeneralProps(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        
        public void SetAirLineAirportConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirlineAirport>()
            .HasKey(t => new { t.AirlinesId, t.AirportsId});


            modelBuilder.Entity<AirlineAirport>()
            .HasOne(pt => pt.Airline)
            .WithMany(p => p.AirlineAirport)
            .HasForeignKey(pt => pt.AirlinesId);

            modelBuilder.Entity<AirlineAirport>()
            .HasOne(pt => pt.Airport)
            .WithMany(p => p.AirlineAirport)
            .HasForeignKey(pt => pt.AirportsId);
        }

        public void RestrictAll(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public void SetConvetionToGeneralProps(ModelBuilder modelBuilder)
        {
            foreach (var props in modelBuilder.Model.GetEntityTypes().Select(e => e.GetProperties()))
            {
                foreach (var prop in props)
                {
                    if (prop.ClrType == typeof(decimal) || prop.ClrType == typeof(decimal?))
                    {
                        prop.SetColumnType("decimal(18,2)");
                    }
                }
            }
        }

        public void SetCountrySeedData(ModelBuilder modelBuilder)
        {
            var countriesJson = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "countries.json"));
            var countries = JsonConvert.DeserializeObject<List<Country>>(countriesJson);
            for (int i = 0; i < countries.Count; i++)
            {
                var country = countries[i];
                country.Id = i+1;
            }
            modelBuilder.Entity<Country>().HasData(countries);
        }

    }
}
