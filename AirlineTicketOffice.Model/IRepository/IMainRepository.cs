using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketOffice.Model.IRepository
{
    public interface IMainRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

    }
}
