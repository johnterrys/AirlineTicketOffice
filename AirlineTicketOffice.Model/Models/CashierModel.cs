using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace AirlineTicketOffice.Model.Models
{
    public class CashierModel:ObservableObject
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

    }
}