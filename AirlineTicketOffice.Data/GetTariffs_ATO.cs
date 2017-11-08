namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GetTariffs_ATO
    {
        [Key]
        [Column(Order = 0)]
        public int RateID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string RateName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1)]
        public string TicketRefund { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string BookingChanges { get; set; }

        [Key]
        [Column(Order = 4)]
        public double BaggageAllowance { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1)]
        public string FreeFood { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(1)]
        public string TypeOfPlace { get; set; }
    }
}
