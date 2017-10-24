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
using System.Diagnostics;

namespace AirlineTicketOffice.Repository.Repositories
{
    public sealed class CashierRepository : BaseModelRepository<Cashier>, ICashierRepository
    {
        public bool Add(CashierModel entity)
        {

            try
            {
                if (entity != null)
                {

                    _context.Cashiers.Add(new Cashier
                    {
                       NumberOfOffices = entity.NumberOfOffices,
                       FullName = entity.FullName
                    });

                    _context.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("'Add(Passenger)' method fail..." + ex.Message);
                return false;
            }
        }

        public IEnumerable<CashierModel> GetAll()
        {
            RefreshAll();

            _context.Database.Log = (s => Console.WriteLine(s));

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

        public bool Remove(CashierModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(CashierModel c)
        {
            if (c != null && c.CashierID > 0)
            {
                try
                {
                    var entity = _context.Cashiers.Where(cas => cas.CashierID == c.CashierID).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.FullName = c.FullName;
                        entity.NumberOfOffices = c.NumberOfOffices;
                    }

                    _context.Entry(entity).State = EntityState.Modified;

                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("'Update(CashierModel c)' method fail..." + ex.Message);
                    return false;
                }

            }
            return false;
        }
    }


}
