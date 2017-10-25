using AirlineTicketOffice.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Model.IRepository
{
    public interface IPlaceInAircraftRepository : IGetAllRepository<PlaceInAircraftModel>
    {
        IEnumerable<PlaceInAircraftModel> GetPlacesOnAircraft(int id);
    }
}
