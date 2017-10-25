using AirlineTicketOffice.Data;
using AirlineTicketOffice.Model.IRepository;
using AirlineTicketOffice.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirlineTicketOffice.Model.Models;

namespace AirlineTicketOffice.Repository.Repositories
{
    public sealed class PlaceInFlightRepository:BaseModelRepository<PlaceInFlight>, IPlaceInFlightRepository
    {

        public PlaceInFlightRepository()
            :base()
        {
        }

        public IEnumerable<PlaceInFlightModel> GetAll()
        {
            //RefreshAll();

            _context.Database.Log = (s => Console.WriteLine(s));

            return _context.PlaceInFlights.AsNoTracking().ToArray().Select((PlaceInFlight p) =>
            {
                return new PlaceInFlightModel
                {
                    TypePlace = p.TypePlace,
                    FlightID = p.FlightID,
                    Amount = p.Amount
                };
            });
        }

        public IEnumerable<PlaceInFlightModel> GetPlacesOnFlight(int id)
        {

            _context.Database.Log = (s => Console.WriteLine(s));

            return _context.PlaceInFlights.AsNoTracking().Where(p => p.FlightID == id).ToArray().Select((PlaceInFlight p) =>
            {
                return new PlaceInFlightModel
                {
                    TypePlace = p.TypePlace,
                    FlightID = p.FlightID,
                    Amount = p.Amount
                };
            });
        }
    }
}