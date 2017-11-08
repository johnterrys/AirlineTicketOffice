namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Passenger")]
    public partial class Passenger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Passenger()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int PassengerID { get; set; }

        [Required]
        [StringLength(50)]
        public string Citizenship { get; set; }

        [Required]
        [StringLength(14)]
        public string PassportNumber { get; set; }

        [Required]
        [StringLength(1)]
        public string Sex { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "date")]
        public DateTime TermOfPassportDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CountryOfResidence { get; set; }

        [StringLength(16)]
        public string PhoneMobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
