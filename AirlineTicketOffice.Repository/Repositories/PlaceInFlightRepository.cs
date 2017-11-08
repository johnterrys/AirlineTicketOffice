using AirlineTicketOffice.Data;
using AirlineTicketOffice.Model.IRepository;
using AirlineTicketOffice.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirlineTicketOffice.Model.Models;
using System.Diagnostics;

namespace AirlineTicketOffice.Repository.Repositories
{
    public sealed class PlaceInFlightRepository:BaseModelRepository<PlaceInFlight>, IPlaceInFlightRepository
    {

        public PlaceInFlightRepository()
            :base()
        {
        }

        /// <summary>
        /// Get all Places In Flights.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlaceInFlightModel> GetAll()
        {
            _methodName = "IEnumerable<PlaceInFlightModel> GetAll() fail...";

            try
            {
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
            catch (NullReferenceException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                DebugWrite(_methodName, ex.Message);
                return null;
            }
           
        }

        /// <summary>
        /// Get Places In specific Flight.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PlaceInFlightModel> GetPlacesOnFlight(int id)
        {
            _methodName = "GetPlacesOnFlight(int id) fail...";

            try
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
            catch (NullReferenceException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                DebugWrite(_methodName, ex.Message);
                return null;
            }
          
        }
    }
}