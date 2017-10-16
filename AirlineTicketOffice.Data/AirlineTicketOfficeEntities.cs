namespace AirlineTicketOffice.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AirlineTicketOfficeEntities : DbContext
    {
        public AirlineTicketOfficeEntities()
            : base("name=AirlineTicketOfficeEntities")
        {
        }

        public virtual DbSet<Aircraft> Aircraft { get; set; }
        public virtual DbSet<Cashier> Cashiers { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<PlaceInAircraft> PlaceInAircrafts { get; set; }
        public virtual DbSet<PlaceInFlight> PlaceInFlights { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<BoughtTickets_ATO> BoughtTickets_ATO { get; set; }
        public virtual DbSet<GetTariffs_ATO> GetTariffs_ATO { get; set; }
        public virtual DbSet<PassangerTicket_ATO> PassangerTicket_ATO { get; set; }
        public virtual DbSet<Passenger_ATO> Passenger_ATO { get; set; }
        public virtual DbSet<Ticket_ATO> Ticket_ATO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>()
                .Property(e => e.TailNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Aircraft>()
                .Property(e => e.TypeOfAircraft)
                .IsUnicode(false);

            modelBuilder.Entity<Aircraft>()
                .HasMany(e => e.Flights)
                .WithRequired(e => e.Aircraft)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Aircraft>()
                .HasMany(e => e.PlaceInAircrafts)
                .WithRequired(e => e.Aircraft)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cashier>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Cashier>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Cashier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                .Property(e => e.FlightNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Flight>()
                .Property(e => e.DepartureTime)
                .HasPrecision(0);

            modelBuilder.Entity<Flight>()
                .Property(e => e.TimeOfArrival)
                .HasPrecision(0);

            modelBuilder.Entity<Flight>()
                .HasMany(e => e.PlaceInFlights)
                .WithRequired(e => e.Flight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Flight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.Citizenship)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.PassportNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.CountryOfResidence)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.PhoneMobile)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Passenger)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlaceInAircraft>()
                .Property(e => e.TypePlace)
                .IsUnicode(false);

            modelBuilder.Entity<PlaceInFlight>()
                .Property(e => e.TypePlace)
                .IsUnicode(false);

            modelBuilder.Entity<Rate>()
                .Property(e => e.RateName)
                .IsUnicode(false);

            modelBuilder.Entity<Rate>()
                .Property(e => e.TicketRefund)
                .IsUnicode(false);

            modelBuilder.Entity<Rate>()
                .Property(e => e.BookingChanges)
                .IsUnicode(false);

            modelBuilder.Entity<Rate>()
                .Property(e => e.FreeFood)
                .IsUnicode(false);

            modelBuilder.Entity<Rate>()
                .Property(e => e.TypeOfPlace)
                .IsUnicode(false);

            modelBuilder.Entity<Rate>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Rate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.NameRoute)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.AirportOfDeparture)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.AirportOfArrival)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.TravelTime)
                .HasPrecision(0);

            modelBuilder.Entity<Route>()
                .Property(e => e.Cost)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Flights)
                .WithRequired(e => e.Route)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.TotalCost)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.FlightNumber)
                .IsUnicode(false);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.TotalCost)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.RateName)
                .IsUnicode(false);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.DepartureTime)
                .HasPrecision(0);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.TimeOfArrival)
                .HasPrecision(0);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.NameRoute)
                .IsUnicode(false);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.AirportOfDeparture)
                .IsUnicode(false);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.AirportOfArrival)
                .IsUnicode(false);

            modelBuilder.Entity<BoughtTickets_ATO>()
                .Property(e => e.TypeOfAircraft)
                .IsUnicode(false);

            modelBuilder.Entity<GetTariffs_ATO>()
                .Property(e => e.RateName)
                .IsUnicode(false);

            modelBuilder.Entity<GetTariffs_ATO>()
                .Property(e => e.TicketRefund)
                .IsUnicode(false);

            modelBuilder.Entity<GetTariffs_ATO>()
                .Property(e => e.BookingChanges)
                .IsUnicode(false);

            modelBuilder.Entity<GetTariffs_ATO>()
                .Property(e => e.FreeFood)
                .IsUnicode(false);

            modelBuilder.Entity<GetTariffs_ATO>()
                .Property(e => e.TypeOfPlace)
                .IsUnicode(false);

            modelBuilder.Entity<PassangerTicket_ATO>()
                .Property(e => e.TotalCost)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PassangerTicket_ATO>()
                .Property(e => e.Citizenship)
                .IsUnicode(false);

            modelBuilder.Entity<PassangerTicket_ATO>()
                .Property(e => e.PassportNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PassangerTicket_ATO>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<PassangerTicket_ATO>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<PassangerTicket_ATO>()
                .Property(e => e.CountryOfResidence)
                .IsUnicode(false);

            modelBuilder.Entity<PassangerTicket_ATO>()
                .Property(e => e.PhoneMobile)
                .IsUnicode(false);

            modelBuilder.Entity<PassangerTicket_ATO>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger_ATO>()
                .Property(e => e.Citizenship)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger_ATO>()
                .Property(e => e.PassportNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger_ATO>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger_ATO>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger_ATO>()
                .Property(e => e.CountryOfResidence)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger_ATO>()
                .Property(e => e.PhoneMobile)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger_ATO>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Ticket_ATO>()
                .Property(e => e.TotalCost)
                .HasPrecision(18, 0);
        }
    }
}
