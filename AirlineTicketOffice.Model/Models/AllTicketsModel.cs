using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace AirlineTicketOffice.Model.Models
{
    public class AllTicketsModel : ObservableObject
    {
        private int ticketID;

        public int TicketID
        {
            get { return ticketID; }
            set { Set(() => TicketID, ref ticketID, value); }
        }

        private int flightID;

        public int FlightID
        {
            get { return flightID; }
            set { Set(() => FlightID, ref flightID, value); }
        }

        private int passengerID;

        public int PassengerID
        {
            get { return passengerID; }
            set { Set(() => PassengerID, ref passengerID, value); }
        }

        private int cashierID;

        public int CashierID
        {
            get { return cashierID; }
            set { Set(() => CashierID, ref cashierID, value); }
        }

        private int rateID;

        public int RateID
        {
            get { return rateID; }
            set { Set(() => RateID, ref rateID, value); }
        }

        private DateTime saleDate;

        public DateTime SaleDate
        {
            get { return saleDate; }
            set { Set(() => SaleDate, ref saleDate, value); }
        }

        private decimal totalCost;

        public decimal TotalCost
        {
            get { return totalCost; }
            set { Set(() => TotalCost, ref totalCost, value); }
        }

        private CashierModel cashier;

        public CashierModel Cashier
        {
            get { return cashier; }
            set { Set(() => Cashier, ref cashier, value); }
        }

        private FlightModel flight;

        public FlightModel Flight
        {
            get { return flight; }
            set { Set(() => Flight, ref flight, value); }
        }

        private PassengerModel passenger;

        public PassengerModel Passenger
        {
            get { return passenger; }
            set { Set(() => Passenger, ref passenger, value); }
        }

        private RateModel rate;

        public RateModel Rate
        {
            get { return rate; }
            set { Set(() => Rate, ref rate, value); }
        }

    }
}