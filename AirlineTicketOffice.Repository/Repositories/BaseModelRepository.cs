using AirlineTicketOffice.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Repository.Repositories
{
    public class BaseModelRepository<TEntity> where TEntity : class
    {
        int res;

        protected readonly AirlineTicketOfficeEntities _context;

        protected BaseModelRepository()
        {
            this._context = new AirlineTicketOfficeEntities();

        }

        /// <summary>
        /// Saves all changes made in this context
        /// to the underlying database.
        /// </summary>
        /// <returns></returns>
        protected bool Save()
        {
            
            try
            {
                if (_context.SaveChanges() == 0)
                {
                    return false;
                }

                return true;
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine("'Save()' method fail..." + ex.Message);
                return false;
            }
            catch (DbEntityValidationException ex)
            {
                Debug.WriteLine("'Save()' method fail..." + ex.Message);
                return false;
            }
            catch (NotSupportedException ex)
            {
                Debug.WriteLine("'Save()' method fail..." + ex.Message);
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                Debug.WriteLine("'Save()' method fail..." + ex.Message);
                return false;
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine("'Save()' method fail..." + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("'Save()' method fail..." + ex.Message);
                return false;
            }
           
        }

        /// <summary>
        /// The All entity will be in the Unchanged state 
        /// after calling this method.
        /// </summary>
        protected void RefreshAll()
        {
            try
            {
                foreach (var entity in _context.ChangeTracker.Entries())
                {
                    entity.Reload();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("'RefreshAll()' method fail..." + ex.Message);
            }
        }

        /// <summary>
        /// Disposes the context.
        /// </summary>
        protected void Close()
        {
            _context.Dispose();
        }

    }
}