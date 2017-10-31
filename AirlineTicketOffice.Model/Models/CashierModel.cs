using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AirlineTicketOffice.Model.Models
{
    public class CashierModel:ObservableObject, IDataErrorInfo
    {
        private int cashierID;

        public int CashierID
        {
            get { return cashierID; }
            set { Set(() => CashierID, ref cashierID, value); }
        }

        private int numberOfOffices;

        public int NumberOfOffices
        {
            get { return numberOfOffices; }
            set { Set(() => NumberOfOffices, ref numberOfOffices, value); }
        }

        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { Set(() => FullName, ref fullName, value); }
        }


        #region realisation IdataErrorInfo
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "NumberOfOffices":
                        if (CheckBlankLine(this.numberOfOffices.ToString()))
                            return "Please enter a Office Number";
                        if (CheckOfficeNumber() == false)
                            return "You must specify the Office Number (Example: 777)";
                        break;
                    case "FullName":
                        if (CheckBlankLine(this.FullName))
                            return "Please enter a Full Name";
                        if (this.FullName.Length < 3 || this.FullName.Length > 50)
                            return "You must specify the name of the country (Example: Finland)";
                        break;                              
                    default:
                        throw new ArgumentException(
                        "Unrecognized property: " + columnName);
                }


                return string.Empty;
            }
        }

        private bool CheckOfficeNumber()
        {
          
            if (this.numberOfOffices < 0 
                || this.numberOfOffices > Int32.MaxValue)
            {
                return false;
            }          

            return true;
        }

        #endregion

        #region methods

        /// <summary>
        /// Check WhiteSpace and Empty string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckBlankLine(string value)
        {
            if (string.IsNullOrWhiteSpace(value)
                || string.IsNullOrEmpty(value))
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}