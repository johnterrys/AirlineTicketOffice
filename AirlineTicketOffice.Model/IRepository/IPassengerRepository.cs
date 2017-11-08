using System;
using System.Collections.Generic;
using System.Linq;
using AirlineTicketOffice.Model.Models;
using System.Text;

namespace AirlineTicketOffice.Model.IRepository
{
    public interface IPassengerRepository : IGetAllRepository<PassengerModel>, IBaseOperationRepository<PassengerModel>
    {
        IEnumerable<PassengerModel> GetAllForRead();
    }
}