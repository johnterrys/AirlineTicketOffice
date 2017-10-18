using AirlineTicketOffice.Data;
using AirlineTicketOffice.Model.IRepository;
using AirlineTicketOffice.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Repository.Repositories
{
    public sealed class TariffsRepository : BaseModelRepository<GetTariffs_ATO>, ITariffsRepository
    {
        public IEnumerable<TariffModel> GetAll()
        {
            return _context.GetTariffs_ATO.AsNoTracking().ToList().Select((GetTariffs_ATO t) =>
            {
                return new TariffModel
                {
                    RateID = t.RateID,
                    RateName = t.RateName,
                    TicketRefund = t.TicketRefund,
                    BookingChanges = t.BookingChanges,
                    BaggageAllowance = t.BaggageAllowance,
                    FreeFood = t.FreeFood,
                    TypeOfPlace = t.TypeOfPlace
                };
            });
        }
    }
}
