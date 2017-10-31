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

        public IEnumerable<PlaceInAircraftModel> GetAll()
        {
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
                Debug.WriteLine("IEnumerable<PlaceInAircraftModel> GetAll() fail..." + ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("IEnumerable<PlaceInAircraftModel> GetAll() fail..." + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("IEnumerable<PlaceInAircraftModel> GetAll() fail..." + ex.Message);
                return null;
            }
            
        }

        public IEnumerable<PlaceInAircraftModel> GetPlacesOnAircraft(int id)
        {
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
                Debug.WriteLine("GetPlacesOnAircraft(int id) fail..." + ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("GetPlacesOnAircraft(int id) fail..." + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetPlacesOnAircraft(int id) fail..." + ex.Message);
                return null;
            }
           
        }
    }
}