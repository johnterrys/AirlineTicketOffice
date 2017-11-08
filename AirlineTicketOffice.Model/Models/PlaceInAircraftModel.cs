using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Model.Models
{
    public class PlaceInAircraftModel:ObservableObject
    {
        private string typePlace;

        public string TypePlace
        {
            get { return typePlace; }
            set { Set(() => TypePlace, ref typePlace, value); }
        }

        private int aircraftID;

        public int AircraftID
        {
            get { return aircraftID; }
            set { Set(() => AircraftID, ref aircraftID, value); }
        }

        private int amount;

        public int Amount
        {
            get { return amount; }
            set { Set(() => Amount, ref amount, value); }
        }

    }
}
