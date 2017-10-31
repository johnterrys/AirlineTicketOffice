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
      

        public IEnumerable<PassengerModel> GetAll()
        {
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
                Debug.WriteLine("IEnumerable<PassengerModel> GetAll() fail..." + ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("IEnumerable<PassengerModel> GetAll() fail..." + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("IEnumerable<PassengerModel> GetAll() fail..." + ex.Message);
                return null;
            }
          
        }

        public IEnumerable<PassengerModel> GetAllForRead()
        {

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
                Debug.WriteLine("IEnumerable<PassengerModel> GetAllForRead() fail..." + ex.Message);
                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("IEnumerable<PassengerModel> GetAllForRead() fail..." + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("IEnumerable<PassengerModel> GetAllForRead() fail..." + ex.Message);
                return null;
            }
          
        }

        public bool Add(PassengerModel entity)
        {

            try
            {
                if (entity != null)
                {

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
                }

                Debug.WriteLine("Add(PassengerModel entity) fail...");
                return false;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Add(PassengerModel entity) fail..." + ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("Add(PassengerModel entity) fail..." + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Add(PassengerModel entity) fail..." + ex.Message);
                return false;
            }
        }

        public bool Remove(PassengerModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(PassengerModel p)
        {
            if (p != null && p.PassengerID > 0)
            {
                try
                {
                    var entity = _context.Passengers.Where(pas => pas.PassengerID == p.PassengerID).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.Citizenship = p.Citizenship;
                        entity.PassportNumber = p.PassportNumber;
                        entity.Sex = p.Sex;
                        entity.FullName = p.FullName;
                        entity.DateOfBirth = p.DateOfBirth;
                        entity.TermOfPassportDate = p.TermOfPassportDate;
                        entity.CountryOfResidence = p.CountryOfResidence;
                        entity.PhoneMobile = p.PhoneMobile;
                        entity.Email = p.Email;
                    }
                   
                    _context.Entry(entity).State = EntityState.Modified;

                    if (Save())
                    {                      
                        return true;
                    }

                    Debug.WriteLine("Update(PassengerModel p) fail...");
                    return false;
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine("Update(PassengerModel p) fail..." + ex.Message);
                    return false;
                }
                catch (ArgumentException ex)
                {
                    Debug.WriteLine("Update(PassengerModel p) fail..." + ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Update(PassengerModel p) fail..." + ex.Message);
                    return false;
                }

            }
            return false;
        }
    }
}