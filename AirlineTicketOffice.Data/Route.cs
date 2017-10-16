namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            Flights = new HashSet<Flight>();
        }

        public int RouteID { get; set; }

        [Required]
        [StringLength(50)]
        public string NameRoute { get; set; }

        [Required]
        [StringLength(50)]
        public string AirportOfDeparture { get; set; }

        [Required]
        [StringLength(50)]
        public string AirportOfArrival { get; set; }

        public TimeSpan TravelTime { get; set; }

        public double Distance { get; set; }

        public decimal Cost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
