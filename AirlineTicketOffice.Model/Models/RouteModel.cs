using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Model.Models
{
    public class RouteModel:ObservableObject
    {
        private int routeID;

        public int RouteID
        {
            get { return routeID; }
            set { Set(() => RouteID, ref routeID, value); }
        }

        private string nameRoute;

        public string NameRoute
        {
            get { return nameRoute; }
            set { Set(() => NameRoute, ref nameRoute, value); }
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

        private System.TimeSpan travelTime;

        public System.TimeSpan TravelTime
        {
            get { return travelTime; }
            set { Set(() => TravelTime, ref travelTime, value); }
        }

        private double distance;

        public double Distance
        {
            get { return distance; }
            set { Set(() => Distance, ref distance, value); }
        }

        // We need create table 'cost route' in database.
        private decimal cost;

        public decimal Cost
        {
            get { return cost; }
            set { Set(() => Cost, ref cost, value); }
        }

    }
}