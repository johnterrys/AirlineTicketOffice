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

namespace AirlineTicketOffice.Repository.Repositories
{
    public sealed class CashierRepository : BaseModelRepository<CashierModel>, ICashierRepository
    {
        public IEnumerable<CashierModel> GetAll()
        {
            return _context.Cashiers.AsNoTracking().ToArray().Select((Cashier c) =>
            {
                return new CashierModel
                {
                    CashierID = c.CashierID,
                    NumberOfOffices = c.NumberOfOffices,
                    FullName = c.FullName
                };
            });
        }
    }


}
