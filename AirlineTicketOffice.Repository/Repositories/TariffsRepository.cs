using AirlineTicketOffice.Data;
using AirlineTicketOffice.Model.IRepository;
using AirlineTicketOffice.Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Repository.Repositories
{
    public sealed class TariffsRepository : BaseModelRepository<GetTariffs_ATO>, ITariffsRepository
    {
        /// <summary>
        /// Get all 'GetTariffs_ATO' in db.(rate via view)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TariffModel> GetAll()
        {
            try
            {
                _context.Database.Log = (s => Console.WriteLine(s));

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
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("IEnumerable<TariffModel> GetAll() fail..." + ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("IEnumerable<TariffModel> GetAll() fail..." + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("IEnumerable<TariffModel> GetAll() fail..." + ex.Message);
                return null;
            }
           
        }
    }
}
