namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Flight")]
    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            PlaceInFlights = new HashSet<PlaceInFlight>();
            Tickets = new HashSet<Ticket>();
        }

        public int FlightID { get; set; }

        [Required]
        [StringLength(10)]
        public string FlightNumber { get; set; }

        public int RouteID { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfDeparture { get; set; }

        public TimeSpan DepartureTime { get; set; }

        public TimeSpan TimeOfArrival { get; set; }

        public int AircraftID { get; set; }

        public virtual Aircraft Aircraft { get; set; }

        public virtual Route Route { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaceInFlight> PlaceInFlights { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
