using AirlineTicketOffice.Model.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirlineTicketOffice.Model.Models;
using System.Linq.Expressions;
using System.Data.Entity;
using AirlineTicketOffice.Data;

namespace AirlineTicketOffice.Repository.Repositories
{
    public sealed class AllTicketsModelRepository : BaseModelRepository<Ticket>, ITicketRepository
    {

        public AllTicketsModelRepository()
            : base()
        {
        }

        public bool Add(AllTicketsModel entity)
        {
            return false;
        }

        public IEnumerable<AllTicketsModel> Find(Expression<Func<AllTicketsModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AllTicketsModel> GetAll()
        {
            _context.Database.Log = (s => Console.WriteLine(s));

            return _context.Tickets.Include(c => c.Cashier).AsNoTracking().ToArray().Select((Ticket t) =>
            {
                return new AllTicketsModel
                {
                    TicketID = t.TicketID,
                    FlightID = t.FlightID,
                    PassengerID = t.PassengerID,
                    CashierID = t.CashierID,
                    RateID = t.RateID,
                    SaleDate = t.SaleDate,
                    TotalCost = t.TotalCost,
                    Cashier = new CashierModel
                    {
                        CashierID = t.Cashier.CashierID,
                        NumberOfOffices = t.Cashier.NumberOfOffices,
                        FullName = t.Cashier.FullName
                    }

                };
            });
        }

        public AllTicketsModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(AllTicketsModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(AllTicketsModel entity)
        {
            throw new NotImplementedException();
        }

       
    }
}
