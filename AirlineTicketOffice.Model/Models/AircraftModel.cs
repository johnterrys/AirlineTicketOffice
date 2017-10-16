using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AirlineTicketOffice.Model.Models
{
    public class AircraftModel: ObservableObject
    {
        private int aircraftID;

        public int AircraftID
        {
            get { return aircraftID; }
            set { Set(() => AircraftID, ref aircraftID, value); }
        }

        private string tailNumber;

        public string TailNumber
        {
            get { return tailNumber; }
            set { Set(() => TailNumber, ref tailNumber, value); }
        }

        private System.DateTime dateOfIssue;

        public System.DateTime DateOfIssue
        {
            get { return dateOfIssue; }
            set { Set(() => DateOfIssue, ref dateOfIssue, value); }
        }

        private string typeOfAircraft;

        public string TypeOfAircraft
        {
            get { return typeOfAircraft; }
            set { Set(() => TypeOfAircraft, ref typeOfAircraft, value); }
        }     

    }
}