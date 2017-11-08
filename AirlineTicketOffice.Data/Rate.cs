namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rate")]
    public partial class Rate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rate()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int RateID { get; set; }

        [Required]
        [StringLength(50)]
        public string RateName { get; set; }

        [Required]
        [StringLength(1)]
        public string TicketRefund { get; set; }

        [Required]
        [StringLength(1)]
        public string BookingChanges { get; set; }

        public double BaggageAllowance { get; set; }

        [Required]
        [StringLength(1)]
        public string FreeFood { get; set; }

        [Required]
        [StringLength(1)]
        public string TypeOfPlace { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
