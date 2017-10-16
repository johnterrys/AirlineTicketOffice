using AirlineTicketOffice.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Model.IRepository
{
    public interface IPlaceInFlightRepository:IMainRepository<PlaceInFlightModel>
    {
        IEnumerable<PlaceInFlightModel> GetPlacesOnFlight(int id);
    }
}
