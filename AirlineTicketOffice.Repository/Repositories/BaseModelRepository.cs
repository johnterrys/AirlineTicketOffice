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

        protected string _methodName;

        protected readonly AirlineTicketOfficeEntities _context;

        protected BaseModelRepository()
        {
            this._context = new AirlineTicketOfficeEntities();
 
        }

        /// <summary>
        /// Check connection with db.
        /// </summary>
        /// <returns></returns>
        protected bool CheckExistDB()
        {
           
            if (_context.Database.Exists())
            {
                return true;
            }

            return false;               
            
        }

        /// <summary>
        /// Saves all changes made in this context
        /// to the underlying database.
        /// </summary>
        /// <returns></returns>
        protected bool Save()
        {
            _methodName = "'Save()' method fail...";

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
                DebugWrite(_methodName, ex.Message);            
                return false;
            }
            catch (DbEntityValidationException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return false;
            }
            catch (NotSupportedException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                DebugWrite(_methodName, ex.Message);
                return false;
            }
            catch (InvalidOperationException ex)
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

        /// <summary>
        /// Write in console fail data.
        /// </summary>
        /// <param name="message"></param>
        protected void DebugWrite(string methodName, string message)
        {
            Debug.WriteLine(methodName + message);
        }

        /// <summary>
        /// The All entity will be in the Unchanged state 
        /// after calling this method.
        /// </summary>
        protected void RefreshAll()
        {
            _methodName = "'RefreshAll()' method fail...";

            try
            {
                foreach (var entity in _context.ChangeTracker.Entries())
                {
                    entity.Reload();
                }
            }
            catch (Exception ex)
            {
                DebugWrite(_methodName, ex.Message);
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