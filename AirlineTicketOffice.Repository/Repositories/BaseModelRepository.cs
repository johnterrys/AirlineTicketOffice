using AirlineTicketOffice.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Repository.Repositories
{
    public class BaseModelRepository<TEntity> where TEntity : class
    {

        protected readonly AirlineTicketOfficeEntities _context;


        public BaseModelRepository()
        {
            this._context = new AirlineTicketOfficeEntities();

        }   

        public int Save()
        {
            return _context.SaveChanges();
        }

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

        protected void Close()
        {
            _context.Dispose();
        }

    }
}