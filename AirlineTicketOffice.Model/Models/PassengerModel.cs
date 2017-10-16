using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Model.Models
{
    public class PassengerModel : ObservableObject
    {
        private int passengerID;

        public int PassengerID
        {
            get { return passengerID; }
            set { Set(() => PassengerID, ref passengerID, value); }
        }

        private string citizenship;

        public string Citizenship
        {
            get { return citizenship; }
            set { Set(() => Citizenship, ref citizenship, value); }
        }

        private string passportNumber;

        public string PassportNumber
        {
            get { return passportNumber; }
            set { Set(() => PassportNumber, ref passportNumber, value); }
        }

        private string sex;

        public string Sex
        {
            get { return sex; }
            set { Set(() => Sex, ref sex, value); }
        }

        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { Set(() => FullName, ref fullName, value); }
        }

        private System.DateTime dateOfBirth;

        public System.DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { Set(() => DateOfBirth, ref dateOfBirth, value); }
        }

        private System.DateTime termOfPassportDate;

        public System.DateTime TermOfPassportDate
        {
            get { return termOfPassportDate; }
            set { Set(() => TermOfPassportDate, ref termOfPassportDate, value); }
        }

        private string countryOfResidence;

        public string CountryOfResidence
        {
            get { return countryOfResidence; }
            set { Set(() => CountryOfResidence, ref countryOfResidence, value); }
        }

        private string phoneMobile;

        public string PhoneMobile
        {
            get { return phoneMobile; }
            set { Set(() => PhoneMobile, ref phoneMobile, value); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { Set(() => Email, ref email, value); }
        }
    }
}