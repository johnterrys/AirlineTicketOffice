namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BoughtTickets_ATO
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string FullName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string FlightNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal TotalCost { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string RateName { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime DateOfDeparture { get; set; }

        [Key]
        [Column(Order = 5)]
        public TimeSpan DepartureTime { get; set; }

        [Key]
        [Column(Order = 6)]
        public TimeSpan TimeOfArrival { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string NameRoute { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string AirportOfDeparture { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string AirportOfArrival { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(50)]
        public string TypeOfAircraft { get; set; }
    }
}
