using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Model.Models
{
    public class FlightModel: ObservableObject
    {
        public FlightModel()
        {
           // this.PlaceInFlights = new HashSet<PlaceInFlight>();
        }

        private int flightID;

        public int FlightID
        {
            get { return flightID; }
            set { Set(() => FlightID, ref flightID, value); }
        }

        private string flightNumber;

        public string FlightNumber
        {
            get { return flightNumber; }
            set { Set(() => FlightNumber, ref flightNumber, value); }
        }

        private int routeID;

        public int RouteID
        {
            get { return routeID; }
            set { Set(() => RouteID, ref routeID, value); }
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

        private int aircraftID;

        public int AircraftID
        {
            get { return aircraftID; }
            set { Set(() => AircraftID, ref aircraftID, value); }
        }

        private AircraftModel aircraft;

        public AircraftModel Aircraft
        {
            get { return aircraft; }
            set { Set(() => Aircraft, ref aircraft, value); }
        }

        private RouteModel route;

        public RouteModel Route
        {
            get { return route; }
            set { Set(() => Route, ref route, value); }
        }

    }
}
