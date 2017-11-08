namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ticket_ATO
    {
        [Key]
        [Column(Order = 0)]
        public int TicketID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlightID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PassengerID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CashierID { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RateID { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "date")]
        public DateTime SaleDate { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal TotalCost { get; set; }
    }
}
