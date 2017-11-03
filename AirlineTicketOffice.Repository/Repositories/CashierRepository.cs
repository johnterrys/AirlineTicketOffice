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
    /// <summary>
    /// Repository 'CashierModel'.
    /// </summary>
    public sealed class CashierRepository : BaseModelRepository<Cashier>, ICashierRepository
    {
        public bool Add(CashierModel entity)
        {
            _methodName = "Add(CashierModel entity) fail...";

            try
            {
                if (entity == null) return false;
                
                _context.Cashiers.Add(new Cashier
                {
                    NumberOfOffices = entity.NumberOfOffices,
                    FullName = entity.FullName
                });

                if (Save())
                {
                    return true;
                }

                DebugWrite(_methodName, String.Empty);
                return false;
            }
            catch (NullReferenceException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                DebugWrite(_methodName, ex.Message);
                return false;
            }
        }

        public IEnumerable<CashierModel> GetAll()
        {
            _methodName = "Add(CashierModel entity) fail...";

            try
            {
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
            catch (NullReferenceException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                DebugWrite(_methodName, ex.Message);
                return null;
            }

        }

        public bool Remove(CashierModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(CashierModel c)
        {

            _methodName = "Update(CashierModel c) fail...";

            try
            {
                if (c == null || c.CashierID <= 0) return false;
                
                var entity = _context.Cashiers.Where(cas => cas.CashierID == c.CashierID).FirstOrDefault();

                if (entity != null)
                {
                    entity.FullName = c.FullName;
                    entity.NumberOfOffices = c.NumberOfOffices;
                }

                _context.Entry(entity).State = EntityState.Modified;

                if (Save())
                {
                    return true;
                }

                DebugWrite(_methodName, String.Empty);
                return false;

            }
            catch (NullReferenceException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                DebugWrite(_methodName, ex.Message);
                return false;
            }

        }

    }
}
