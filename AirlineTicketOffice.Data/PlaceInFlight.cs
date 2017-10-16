namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlaceInFlight")]
    public partial class PlaceInFlight
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string TypePlace { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlightID { get; set; }

        public int Amount { get; set; }

        public virtual Flight Flight { get; set; }
    }
}
