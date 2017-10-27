using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace AirlineTicketOffice.Model.Models
{
    public class BoughtTicketModel:ObservableObject
    {
        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { Set(() => FullName, ref fullName, value); }
        }

        private string _passportNumber;

        public string PassportNumber
        {
            get { return _passportNumber; }
            set { Set(() => PassportNumber, ref _passportNumber, value); }
        }

        private string flightNumber;

        public string FlightNumber
        {
            get { return flightNumber; }
            set { Set(() => FlightNumber, ref flightNumber, value); }
        }

        private decimal totalCost;

        public decimal TotalCost
        {
            get { return totalCost; }
            set { Set(() => TotalCost, ref totalCost, value); }
        }

        private System.DateTime _saleDate;

        public System.DateTime SaleDate
        {
            get { return _saleDate; }
            set { Set(() => SaleDate, ref _saleDate, value); }
        }

        private string rateName;

        public string RateName
        {
            get { return rateName; }
            set { Set(() => RateName, ref rateName, value); }
        }

        private System.DateTime dateOfDeparture;

        public System.DateTime DateOfDeparture
        {
            get { return dateOfDeparture; }
            set { Set(() => DateOfDeparture, ref dateOfDeparture, value); }
        }

        private System.TimeSpan departureTime;

        public System.TimeSpan DepartureTime
        {
            get { return departureTime; }
            set { Set(() => DepartureTime, ref departureTime, value); }
        }

        private System.TimeSpan timeOfArrival;

        public System.TimeSpan TimeOfArrival
        {
            get { return timeOfArrival; }
            set { Set(() => TimeOfArrival, ref timeOfArrival, value); }
        }

        private string nameRoute;

        public string NameRoute
        {
            get { return nameRoute; }
            set { Set(() => nameRoute, ref nameRoute, value); }
        }

        private string airportOfDeparture;

        public string AirportOfDeparture
        {
            get { return airportOfDeparture; }
            set { Set(() => AirportOfDeparture, ref airportOfDeparture, value); }
        }

        private string airportOfArrival;

        public string AirportOfArrival
        {
            get { return airportOfArrival; }
            set { Set(() => AirportOfArrival, ref airportOfArrival, value); }
        }

        private TimeSpan _travelTime;

        public TimeSpan TravelTime
        {
            get { return _travelTime; }
            set { Set(() => TravelTime, ref _travelTime, value); }
        }

        private double _distance;

        public double Distance
        {
            get { return _distance; }
            set { Set(() => Distance, ref _distance, value); }
        }
        

        private string typeOfAircraft;

        public string TypeOfAircraft
        {
            get { return typeOfAircraft; }
            set { Set(() => TypeOfAircraft, ref typeOfAircraft, value); }
        }

        private string _cashierFullName;

        public string CashierFullName
        {
            get { return _cashierFullName; }
            set { Set(() => CashierFullName, ref _cashierFullName, value); }
        }

        private int _numberOfOffices;

        public int NumberOfOffices
        {
            get { return _numberOfOffices; }
            set { Set(() => NumberOfOffices, ref _numberOfOffices, value); }
        }
    }
}
