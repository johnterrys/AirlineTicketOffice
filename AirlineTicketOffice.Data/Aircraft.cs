namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Aircraft
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aircraft()
        {
            Flights = new HashSet<Flight>();
            PlaceInAircrafts = new HashSet<PlaceInAircraft>();
        }

        public int AircraftID { get; set; }

        [Required]
        [StringLength(10)]
        public string TailNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfIssue { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeOfAircraft { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flight> Flights { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaceInAircraft> PlaceInAircrafts { get; set; }
    }
}
