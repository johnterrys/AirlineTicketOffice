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
    public class PlaceInAircraftRepository : BaseModelRepository<PlaceInAircraft>, IPlaceInAircraftRepository
    {

        public PlaceInAircraftRepository()
            :base()
        {
        }

        /// <summary>
        /// Get All places in Aircraft.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlaceInAircraftModel> GetAll()
        {
            _methodName = "IEnumerable<PlaceInAircraftModel> GetAll() fail...";

            try
            {
                _context.Database.Log = (s => Console.WriteLine(s));

                return _context.PlaceInAircrafts.AsNoTracking().ToArray().Select((PlaceInAircraft p) =>
                {
                    return new PlaceInAircraftModel
                    {
                        TypePlace = p.TypePlace,
                        AircraftID = p.AircraftID,
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
        /// Get Places In specific Aircraft.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PlaceInAircraftModel> GetPlacesOnAircraft(int id)
        {
            _methodName = "GetPlacesOnAircraft(int id) fail...";

            try
            {
                _context.Database.Log = (s => Console.WriteLine(s));

                return _context.PlaceInAircrafts.AsNoTracking().Where(p => p.AircraftID == id).ToArray().Select((PlaceInAircraft p) =>
                {
                    return new PlaceInAircraftModel
                    {
                        TypePlace = p.TypePlace,
                        AircraftID = p.AircraftID,
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