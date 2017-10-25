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
    public sealed class BoughtTicketRepository : BaseModelRepository<Ticket>, IBoughtTicketRepository
    {
        public IEnumerable<BoughtTicketModel> GetAll()
        {
            _context.Database.Log = (s => Console.WriteLine(s));

            return _context.BoughtTickets_ATO.AsNoTracking().ToList().Select((BoughtTickets_ATO b) =>
            {
                return new BoughtTicketModel
                {
                    FullName = b.FullName,
                    FlightNumber = b.FlightNumber,
                    TotalCost = b.TotalCost,
                    RateName = b.RateName,
                    DateOfDeparture = b.DateOfDeparture,
                    DepartureTime = b.DepartureTime,
                    TimeOfArrival = b.TimeOfArrival,
                    NameRoute = b.NameRoute,
                    AirportOfDeparture = b.AirportOfDeparture,
                    AirportOfArrival = b.AirportOfArrival,
                    TypeOfAircraft = b.TypeOfAircraft
                };
            });
        }
    }
}