using AirlineTicketOffice.Model.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirlineTicketOffice.Model.Models;
using System.Linq.Expressions;
using System.Data.Entity;
using AirlineTicketOffice.Data;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AirlineTicketOffice.Repository.Repositories
{
    public sealed class FlightModelRepository : BaseModelRepository<Flight>, IFlightRepository
    {


        public FlightModelRepository()
             : base()
        {
        }

        /// <summary>
        /// Get all 'flights' from db into 'FlightModel'.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FlightModel> GetAll()
        {
            _methodName = "IEnumerable<FlightModel> GetAll fail...";

            try
            {

                _context.Database.Log = (s => Console.WriteLine(s));

                return _context.Flights.Include(a => a.Aircraft).Include(r => r.Route).ToList().Select((Flight f) =>
                {
                    return new FlightModel
                    {
                        FlightID = f.FlightID,
                        FlightNumber = f.FlightNumber,
                        RouteID = f.RouteID,
                        DateOfDeparture = f.DateOfDeparture,
                        DepartureTime = f.DepartureTime,
                        TimeOfArrival = f.TimeOfArrival,
                        AircraftID = f.AircraftID,
                        Aircraft = new AircraftModel
                        {
                            AircraftID = f.Aircraft.AircraftID,
                            TailNumber = f.Aircraft.TailNumber,
                            DateOfIssue = f.Aircraft.DateOfIssue,
                            TypeOfAircraft = f.Aircraft.TypeOfAircraft
                        },
                        Route = new RouteModel
                        {
                            RouteID = f.Route.RouteID,
                            NameRoute = f.Route.NameRoute,
                            AirportOfDeparture = f.Route.AirportOfDeparture,
                            AirportOfArrival = f.Route.AirportOfArrival,
                            TravelTime = f.Route.TravelTime,
                            Distance = f.Route.Distance,
                            Cost = f.Route.Cost
                        }
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
