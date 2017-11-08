using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Diagnostics;

namespace AirlineTicketOffice.Model.Models
{
    public class AllTicketsModel : ObservableObject
    {
        private int ticketID;

        public int TicketID
        {
            get { return ticketID; }
            set { Set(() => TicketID, ref ticketID, value); }
        }

        private int flightID;

        public int FlightID
        {
            get { return flightID; }
            set { Set(() => FlightID, ref flightID, value); }
        }

        private int passengerID;

        public int PassengerID
        {
            get { return passengerID; }
            set { Set(() => PassengerID, ref passengerID, value); }
        }

        private int cashierID;

        public int CashierID
        {
            get { return cashierID; }
            set { Set(() => CashierID, ref cashierID, value); }
        }

        private int rateID;

        public int RateID
        {
            get { return rateID; }
            set { Set(() => RateID, ref rateID, value); }
        }

        private DateTime saleDate;

        public DateTime SaleDate
        {
            get { return saleDate; }
            set { Set(() => SaleDate, ref saleDate, value); }
        }

        private decimal totalCost;

        public decimal TotalCost
        {
            get { return totalCost; }
            set { Set(() => TotalCost, ref totalCost, value); }
        }

        private CashierModel cashier;

        public CashierModel Cashier
        {
            get { return cashier; }
            set { Set(() => Cashier, ref cashier, value); }
        }

        private FlightModel flight;

        public FlightModel Flight
        {
            get { return flight; }
            set { Set(() => Flight, ref flight, value); }
        }

        private PassengerModel passenger;

        public PassengerModel Passenger
        {
            get { return passenger; }
            set { Set(() => Passenger, ref passenger, value); }
        }

        private TariffModel rate;

        public TariffModel Rate
        {
            get { return rate; }
            set { Set(() => Rate, ref rate, value); }
        }



        #region methods

        public static bool CheckNewTicket(AllTicketsModel ticket)
        {
            CheckResult(ticket.totalCost);

            if (ticket == null
                || ticket.Rate == null
                || ticket.Passenger == null
                || ticket.flight == null
                || ticket.Cashier == null)
            {
                return false;
            }
            if (ticket.totalCost <= Decimal.Zero)
            {
                return false;
            }
            if (ticket.saleDate.Date < DateTime.Now.Date)
            {
                return false;
            }

            return true;
        }

        public static decimal CalculateFullCost(FlightModel flight, TariffModel tariff)
        {
            try
            {

                decimal fullCost = Decimal.Zero;
              
                if (tariff.TypeOfPlace.ToUpper() == "A")
                {
                    fullCost = Decimal.Add(Decimal.Multiply(flight.Route.Cost, 0.4m), flight.Route.Cost);

                    return CheckResult(fullCost);
                }
                if (tariff.TypeOfPlace.ToUpper() == "B")
                {
                    fullCost = Decimal.Add(Decimal.Multiply(flight.Route.Cost, 0.2m), flight.Route.Cost);

                    return CheckResult(fullCost);

                }
                else
                {
                    return Decimal.MinusOne;
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("CalculateFullCost(FlightModel flight, TariffModel tariff) method fail..." + ex.Message);

                return Decimal.MinusOne;
            }
            catch (ArithmeticException ex)
            {
                Debug.WriteLine("CalculateFullCost(FlightModel flight, TariffModel tariff) method fail..." + ex.Message);

                return Decimal.MinusOne;
            }
           
        }

        ///* Check result operation. */
        private static decimal CheckResult(decimal d)
        {
            if (d <= Decimal.Zero || d > Decimal.MaxValue)
            {
                throw new ArithmeticException();
            }
            else
                return d;
        }

        #endregion

    }
}