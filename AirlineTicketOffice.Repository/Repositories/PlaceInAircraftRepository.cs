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
    public class PlaceInAircraftRepository : BaseModelRepository<PlaceInAircraft>, IPlaceInAircraftRepository
    {

        public PlaceInAircraftRepository()
            :base()
        {
        }

        public IEnumerable<PlaceInAircraftModel> GetAll()
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

        public IEnumerable<PlaceInAircraftModel> GetPlacesOnAircraft(int id)
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
    }
}