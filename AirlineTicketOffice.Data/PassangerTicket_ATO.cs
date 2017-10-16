namespace AirlineTicketOffice.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PassangerTicket_ATO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string Citizenship { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(14)]
        public string PassportNumber { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(1)]
        public string Sex { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string FullName { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "date")]
        public DateTime TermOfPassportDate { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(50)]
        public string CountryOfResidence { get; set; }

        [StringLength(16)]
        public string PhoneMobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
