using AirlineTicketOffice.Model.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirlineTicketOffice.Model.Models;
using System.Linq.Expressions;
using System.Data.Entity;
using AirlineTicketOffice.Data;
using System.Diagnostics;

namespace AirlineTicketOffice.Repository.Repositories
{
    public sealed class PassengerModelRepository : BaseModelRepository<Passenger>, IPassengerRepository
    {

        /// <summary>
        /// Get all passenger from db into passengerModel.
        /// </summary>
        /// <returns>IEnumerable<PassengerModel></returns>
        public IEnumerable<PassengerModel> GetAll()
        {
            _methodName = "IEnumerable<PassengerModel> GetAll() fail...";

            try
            {
                _context.Database.Log = (s => Console.WriteLine(s));

                return _context.Passengers.ToList().Select((Passenger p) =>
                {
                    return new PassengerModel
                    {
                        PassengerID = p.PassengerID,
                        Citizenship = p.Citizenship,
                        PassportNumber = p.PassportNumber,
                        Sex = p.Sex,
                        FullName = p.FullName,
                        DateOfBirth = p.DateOfBirth,
                        TermOfPassportDate = p.TermOfPassportDate,
                        CountryOfResidence = p.CountryOfResidence,
                        PhoneMobile = p.PhoneMobile,
                        Email = p.Email

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

        public IEnumerable<PassengerModel> GetAllForRead()
        {
            _methodName = "IEnumerable<PassengerModel> GetAllForRead() fail...";

            try
            {
                _context.Database.Log = (s => Console.WriteLine(s));

                return _context.Passenger_ATO.AsNoTracking().ToList().Select((Passenger_ATO p) =>
                {
                    return new PassengerModel
                    {
                        PassengerID = p.PassengerID,
                        Citizenship = p.Citizenship,
                        PassportNumber = p.PassportNumber,
                        Sex = p.Sex,
                        FullName = p.FullName,
                        DateOfBirth = p.DateOfBirth,
                        TermOfPassportDate = p.TermOfPassportDate,
                        CountryOfResidence = p.CountryOfResidence,
                        PhoneMobile = p.PhoneMobile,
                        Email = p.Email

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

        /// <summary>
        /// Add passenger into db.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(PassengerModel entity)
        {

            _methodName = "Add(PassengerModel entity) fail...";

            try
            {
                if (entity == null) return false;

                _context.Passengers.Add(new Passenger
                    {
                        Citizenship = entity.Citizenship,
                        PassportNumber = entity.PassportNumber,
                        Sex = entity.Sex.ToUpper(),
                        FullName = entity.FullName,
                        DateOfBirth = entity.DateOfBirth,
                        TermOfPassportDate = entity.TermOfPassportDate,
                        CountryOfResidence = entity.CountryOfResidence,
                        PhoneMobile = entity.PhoneMobile,
                        Email = entity.Email
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

        public bool Remove(PassengerModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update 'Passenger' in db.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Update(PassengerModel p)
        {
            _methodName = "Update(PassengerModel p) fail...";

            try
            {
                if (p == null || p.PassengerID <= 0) return false;
                
                var entity = _context.Passengers.Where(pas => pas.PassengerID == p.PassengerID).FirstOrDefault();
                  
                entity.Citizenship = p.Citizenship;
                entity.PassportNumber = p.PassportNumber;
                entity.Sex = p.Sex;
                entity.FullName = p.FullName;
                entity.DateOfBirth = p.DateOfBirth;
                entity.TermOfPassportDate = p.TermOfPassportDate;
                entity.CountryOfResidence = p.CountryOfResidence;
                entity.PhoneMobile = p.PhoneMobile;
                entity.Email = p.Email;
                    
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