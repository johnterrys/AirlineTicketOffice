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
        [StringLength(14)]
        public string PassportNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string FlightNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal TotalCost { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime SaleDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string RateName { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "date")]
        public DateTime DateOfDeparture { get; set; }

        [Key]
        [Column(Order = 7)]
        public TimeSpan DepartureTime { get; set; }

        [Key]
        [Column(Order = 8)]
        public TimeSpan TimeOfArrival { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string NameRoute { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(50)]
        public string AirportOfDeparture { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(50)]
        public string AirportOfArrival { get; set; }

        [Key]
        [Column(Order = 12)]
        public TimeSpan TravelTime { get; set; }

        [Key]
        [Column(Order = 13)]
        public double Distance { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(50)]
        public string TypeOfAircraft { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(50)]
        public string CashierFullName { get; set; }

        [Key]
        [Column(Order = 16)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumberOfOffices { get; set; }
    }
}
