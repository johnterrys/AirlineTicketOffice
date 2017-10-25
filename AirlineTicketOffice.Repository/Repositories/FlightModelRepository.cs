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

        public IEnumerable<FlightModel> GetAll()
        {
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
                Debug.WriteLine("IEnumerable<FlightModel> GetAll fail..." + ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("IEnumerable<FlightModel> GetAll fail..." + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("IEnumerable<FlightModel> GetAll fail..." + ex.Message);
                return null;
            }
          
        }


    }
}
