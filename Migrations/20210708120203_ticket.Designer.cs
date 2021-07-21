﻿// <auto-generated />
using System;
using AireLineTicketSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AireLineTicketSystem.Migrations
{
    [DbContext(typeof(AireLineTicketSystemContext))]
    [Migration("20210708120203_ticket")]
    partial class ticket
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AireLineTicketSystem.Entities.Airline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Airplane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirlineId")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalSeats")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.ToTable("Airplanes");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

             
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Gate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirportId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.HasIndex("AirportId");

                    b.ToTable("Gates");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Passenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryPassportId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PassportCode")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("CountryPassportId");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Terminal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirlineId")
                        .HasColumnType("int");

                    b.Property<int>("AirportId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("AirportId");

                    b.ToTable("Terminals");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirlineId")
                        .HasColumnType("int");

                    b.Property<int>("AirplaneId")
                        .HasColumnType("int");

                    b.Property<int>("AirportArrivalId")
                        .HasColumnType("int");

                    b.Property<int>("AirportDepartureId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DepartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GateId")
                        .HasColumnType("int");

                    b.Property<decimal>("MinPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PassengerId")
                        .HasColumnType("int");

                    b.Property<int>("TerminalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("AirplaneId");

                    b.HasIndex("AirportArrivalId");

                    b.HasIndex("AirportDepartureId");

                    b.HasIndex("GateId");

                    b.HasIndex("PassengerId");

                    b.HasIndex("TerminalId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("AirlineAirport", b =>
                {
                    b.Property<int>("AirlinesId")
                        .HasColumnType("int");

                    b.Property<int>("AirportsId")
                        .HasColumnType("int");

                    b.HasKey("AirlinesId", "AirportsId");

                    b.HasIndex("AirportsId");

                    b.ToTable("AirlineAirport");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Airplane", b =>
                {
                    b.HasOne("AireLineTicketSystem.Entities.Airline", "Airline")
                        .WithMany("Airplanes")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Airline");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Airport", b =>
                {
                    b.HasOne("AireLineTicketSystem.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Gate", b =>
                {
                    b.HasOne("AireLineTicketSystem.Entities.Airport", "Airport")
                        .WithMany()
                        .HasForeignKey("AirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Airport");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Passenger", b =>
                {
                    b.HasOne("AireLineTicketSystem.Entities.Country", "CountryPassport")
                        .WithMany()
                        .HasForeignKey("CountryPassportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CountryPassport");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Terminal", b =>
                {
                    b.HasOne("AireLineTicketSystem.Entities.Airline", "Airline")
                        .WithMany("Terminals")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AireLineTicketSystem.Entities.Airport", "Airport")
                        .WithMany("Terminals")
                        .HasForeignKey("AirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Airline");

                    b.Navigation("Airport");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Ticket", b =>
                {
                    b.HasOne("AireLineTicketSystem.Entities.Airline", "Airline")
                        .WithMany("Tickets")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AireLineTicketSystem.Entities.Airplane", "Airplane")
                        .WithMany("Tickets")
                        .HasForeignKey("AirplaneId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AireLineTicketSystem.Entities.Airport", "AirportArrival")
                        .WithMany()
                        .HasForeignKey("AirportArrivalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AireLineTicketSystem.Entities.Airport", "AirportDeparture")
                        .WithMany()
                        .HasForeignKey("AirportDepartureId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AireLineTicketSystem.Entities.Gate", "Gate")
                        .WithMany("Tickets")
                        .HasForeignKey("GateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AireLineTicketSystem.Entities.Passenger", "Passenger")
                        .WithMany("Tickets")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AireLineTicketSystem.Entities.Terminal", "Terminal")
                        .WithMany()
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Airline");

                    b.Navigation("Airplane");

                    b.Navigation("AirportArrival");

                    b.Navigation("AirportDeparture");

                    b.Navigation("Gate");

                    b.Navigation("Passenger");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("AirlineAirport", b =>
                {
                    b.HasOne("AireLineTicketSystem.Entities.Airline", null)
                        .WithMany()
                        .HasForeignKey("AirlinesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AireLineTicketSystem.Entities.Airport", null)
                        .WithMany()
                        .HasForeignKey("AirportsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Airline", b =>
                {
                    b.Navigation("Airplanes");

                    b.Navigation("Terminals");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Airplane", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Airport", b =>
                {
                    b.Navigation("Terminals");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Gate", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("AireLineTicketSystem.Entities.Passenger", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}