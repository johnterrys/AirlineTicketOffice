using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Model.IRepository
{
    public interface IBaseOperationRepository<TEntity> where TEntity : class
    {

        bool Add(TEntity entity);

        bool Remove(TEntity entity);

        bool Update(TEntity entity);
    }
}