namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        public int TicketID { get; set; }

        public int FlightID { get; set; }

        public int PassengerID { get; set; }

        public int CashierID { get; set; }

        public int RateID { get; set; }

        [Column(TypeName = "date")]
        public DateTime SaleDate { get; set; }

        public decimal TotalCost { get; set; }

        public virtual Cashier Cashier { get; set; }

        public virtual Flight Flight { get; set; }

        public virtual Passenger Passenger { get; set; }

        public virtual Rate Rate { get; set; }
    }
}
