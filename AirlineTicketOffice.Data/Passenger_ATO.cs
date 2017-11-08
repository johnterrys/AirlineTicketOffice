namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Passenger_ATO
    {
        [Key]
        [Column(Order = 0)]
        public int PassengerID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Citizenship { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(14)]
        public string PassportNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string Sex { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string FullName { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "date")]
        public DateTime TermOfPassportDate { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string CountryOfResidence { get; set; }

        [StringLength(16)]
        public string PhoneMobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
