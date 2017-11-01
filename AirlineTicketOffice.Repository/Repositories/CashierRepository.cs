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

            try
            {
                if (entity != null)
                {

                    _context.Cashiers.Add(new Cashier
                    {
                        NumberOfOffices = entity.NumberOfOffices,
                        FullName = entity.FullName
                    });

                    if (Save())
                    {
                        return true;
                    }

                }

                Debug.WriteLine("Update(CashierModel c) fail...");
                return false;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Add(CashierModel entity) fail..." + ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("Add(CashierModel entity)fail..." + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Add(CashierModel entity) fail..." + ex.Message);
                return false;
            }
        }

        public IEnumerable<CashierModel> GetAll()
        {
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
                Debug.WriteLine("Add(CashierModel entity) fail..." + ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("Add(CashierModel entity)fail..." + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Add(CashierModel entity) fail..." + ex.Message);
                return null;
            }

        }

        public bool Remove(CashierModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(CashierModel c)
        {

            try
            {
                if (c != null && c.CashierID > 0)
                {

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
                }

                Debug.WriteLine("Update(CashierModel c) fail...");
                return false;

            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Update(CashierModel c) fail..." + ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("Update(CashierModel c) fail..." + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Update(CashierModel c) fail..." + ex.Message);
                return false;
            }

        }

    }
}
