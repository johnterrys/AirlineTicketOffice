using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Model.Models
{
    public class RateModel : ObservableObject
    {
        private int rateID;

        public int RateID
        {
            get { return rateID; }
            set { Set(() => RateID, ref rateID, value); }
        }

        private string rateName;

        public string RateName
        {
            get { return rateName; }
            set { Set(() => RateName, ref rateName, value); }
        }

        private string ticketRefund;

        public string TicketRefund
        {
            get { return ticketRefund; }
            set { Set(() => TicketRefund, ref ticketRefund, value); }
        }

        private string bookingChanges;

        public string BookingChanges
        {
            get { return bookingChanges; }
            set { Set(() => BookingChanges, ref bookingChanges, value); }
        }

        private double baggageAllowance;

        public double BaggageAllowance
        {
            get { return baggageAllowance; }
            set { Set(() => BaggageAllowance, ref baggageAllowance, value); }
        }

        private string freeFood;

        public string FreeFood
        {
            get { return freeFood; }
            set { Set(() => FreeFood, ref freeFood, value); }
        }

        private string typeOfPlace;

        public string TypeOfPlace
        {
            get { return typeOfPlace; }
            set { Set(() => TypeOfPlace, ref typeOfPlace, value); }
        }
    }
}
