using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AirlineTicketOffice.Model.Models
{
    public class PlaceInFlightModel:ObservableObject
    {
        private string typePlace;

        public string TypePlace
        {
            get { return typePlace; }
            set { Set(() => TypePlace, ref typePlace, value); }
        }

        private int flightID;

        public int FlightID
        {
            get { return flightID; }
            set { Set(() => FlightID, ref flightID, value); }
        }

        private int amount;

        public int Amount
        {
            get { return amount; }
            set { Set(() => Amount, ref amount, value); }
        }

    }
}