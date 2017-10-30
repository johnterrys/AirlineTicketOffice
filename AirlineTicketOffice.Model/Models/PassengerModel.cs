using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AirlineTicketOffice.Model.Models
{
    public class PassengerModel : ObservableObject, IDataErrorInfo
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


        /// <summary>
        /// Implementation dataRrrorInfo.
        /// </summary>
        public string Error
        {
            get { return null; }
        }

        /// <summary>
        /// Implementation dataRrrorInfo.
        /// </summary>
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Citizenship":
                        if (CheckString(this.FullName) == false || this.Citizenship.Length < 3)
                            return "You must specify the name of the country (Example: Finland)";
                        break;
                    case "PassportNumber":
                        if (CheckString(this.FullName) == false || CheckPassportRegex() == false)
                            return "You must specify the passport number of the citizen (Example: 5040979Е028РВ8)";
                        break;
                    case "Sex":
                        if (CheckString(this.Sex) == false || CheckGender() == false)
                            return "You must specify the sex of the citizen (Example: M or W)";
                        break;
                    case "FullName":
                        if (CheckString(this.FullName) == false || this.FullName.Length < 3)
                            return "You must specify the Full Name of the citizen (Example: Ivanov Ivan Ivanovich)";
                        break;                 
                    case "CountryOfResidence":
                        if (CheckString(this.FullName) == false || this.CountryOfResidence.Length < 3)
                            return "You must specify the name of the country (Example: England)";
                        break;
                    case "PhoneMobile":
                        if (CheckPhone() == false)
                            return "You must specify the mobile phone of the citizen (Example: 37533-566-56-54)";
                        break;
                    case "Email":
                        if (CheckEmail() == false)
                            return "You must specify the Email of the citizen (Example: ivanov@gmail.com)";
                        break;
                    default:
                        throw new ArgumentException(
                        "Unrecognized property: " + columnName);
                }


                return string.Empty;
            }
        }

       
        #region methods

        /// <summary>
        /// Check passport number.
        /// </summary>
        /// <returns></returns>
        private bool CheckPassportRegex()
        {
            Regex rgx = new Regex(@"^\d{7}[A-Z]\d{3}[A-Z][A-Z]\d$");

            if (rgx.IsMatch(this.PassportNumber))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check gender of citizen.
        /// </summary>
        /// <returns></returns>
        private bool CheckGender()
        {
            if (this.Sex.ToUpper() == "W" || this.Sex.ToUpper() == "M")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check WhiteSpace and Empty string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckString(string value)
        {
            if (string.IsNullOrWhiteSpace(value)
                || string.IsNullOrEmpty(value))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check mobile phone.
        /// </summary>
        /// <returns></returns>
        private bool CheckPhone()
        {
            if (CheckString(this.PhoneMobile) == true
                && this.phoneMobile.Length > 16)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check email.
        /// </summary>
        /// <returns></returns>
        private bool CheckEmail()
        {

            if (CheckString(this.Email) == true
                && this.Email.Length > 50)
            {
                return false;
            }

            return true;
        }



        #endregion
    }
}