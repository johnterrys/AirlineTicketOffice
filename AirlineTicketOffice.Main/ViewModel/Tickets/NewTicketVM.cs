using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using AirlineTicketOffice.Repository.Repositories;
using AirlineTicketOffice.Model.IRepository;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AirlineTicketOffice.Model.Models;
using GalaSoft.MvvmLight.Messaging;
using AirlineTicketOffice.Main.Services.Messenger;
using AirlineTicketOffice.Main.Services.Navigation;
using System.Threading.Tasks;
using System.Windows;

namespace AirlineTicketOffice.Main.ViewModel.Tickets
{
    public sealed class NewTicketVM:ViewModelBase
    {
        #region constructor
        public NewTicketVM(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;

            this.SaleDate = DateTime.Now;

            this.NewTicket = new AllTicketsModel();

            this.ForegroundForUser = "#f2f2f2";
            this.MessageForUser = "At First You Need Select The Flight.";

            ReceiveFlightFromFlightVM();
            ReceivePassengerFromPassengerVM();
            ReceiveCashierFromCashierVM();
            ReceiveTariffFromRateVM();

        }

      
        #endregion

        #region fields

        private readonly ITicketRepository _ticketRepository;        

        private AllTicketsModel _newTicket;

        private string _ForegroundForUser;

        private string _MessageForUser;

        private FlightModel _flight;

        private PassengerModel _passenger;

        private CashierModel _cashier;

        private TariffModel _tariff;

        private DateTime _saleDate;

        private decimal _fullCost;

        #endregion

        #region properties

        public decimal FullCost
        {
            get { return _fullCost; }
            set { Set(() => FullCost, ref _fullCost, value); }
        }

        public DateTime SaleDate
        {
            get { return _saleDate; }
            set { Set(() => SaleDate, ref _saleDate, value); }
        }

        public TariffModel Tariff
        {
            get { return _tariff; }
            set { Set(() => Tariff, ref _tariff, value); }
        }

        public CashierModel Cashier
        {
            get { return _cashier; }
            set { Set(() => Cashier, ref _cashier, value); }
        }

        public PassengerModel Passenger
        {
            get { return _passenger; }
            set { Set(() => Passenger, ref _passenger, value); }
        }

        public FlightModel Flight
        {
            get { return _flight; }
            set { Set(() => Flight, ref _flight, value); }
        }

        public AllTicketsModel NewTicket
        {
            get { return _newTicket; }
            set { Set(() => NewTicket, ref _newTicket, value); }
        }

        public string ForegroundForUser
        {
            get { return _ForegroundForUser; }
            set { Set(() => ForegroundForUser, ref _ForegroundForUser, value); }
        }

        public string MessageForUser
        {
            get { return _MessageForUser; }
            set { Set(() => MessageForUser, ref _MessageForUser, value); }
        }


        #endregion

        #region commands 

        private ICommand _fullCostCommand;

        public ICommand FullCostCommand
        {
            get
            {
                if (_fullCostCommand == null)
                {
                    _fullCostCommand = new RelayCommand(() =>
                    {

                        CheckCost();                      
                        
                    });
                }
                return _fullCostCommand;
            }

            set { _fullCostCommand = value; }
        }


        /// <summary>
        /// Create new ticket in db via repository.
        /// </summary>
        private ICommand _saveNewTicketCommand;

        public ICommand SaveNewTicketCommand
        {
            get
            {
                if (_saveNewTicketCommand == null)
                {
                    _saveNewTicketCommand = new RelayCommand(() =>
                    {

                        try
                        {

                            this.NewTicket.SaleDate = this.SaleDate;
                            this.NewTicket.TotalCost = this.FullCost;                            
                                                       
                            if (AllTicketsModel.CheckNewTicket(this.NewTicket)
                                && _ticketRepository.Add(this.NewTicket))
                            {

                                RaisePropertyChanged("NewTicket");                             
                                this.MessageForUser = "Inserting of data has passed successfully..";
                                this.ForegroundForUser = "#68a225";
                            }
                            else
                            {
                                this.MessageForUser = "Inserting Data Is Not Passed.";
                                this.ForegroundForUser = "#ff420e";
                            }
                        }
                        catch (ArithmeticException ex)
                        {
                            this.MessageForUser = "Inserting Data Is Not Passed.";
                            this.ForegroundForUser = "#ff420e";
                            Debug.WriteLine("'SaveNewTicketCommand' fail..." + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            this.MessageForUser = "Inserting Data Is Not Passed.";
                            this.ForegroundForUser = "#ff420e";
                            Debug.WriteLine("'SaveNewTicketCommand' fail..." + ex.Message);
                        }

                    });
                }
                return _saveNewTicketCommand;
            }
            set { _saveNewTicketCommand = value; }
        }     

        #endregion

        #region methods      

        /// <summary>
        /// Receive 'FlightModel' from SendNewTicketCommand(flight view model)
        /// </summary>
        private void ReceiveFlightFromFlightVM()
        {
            Messenger.Default.Register<MessageFlightToNewTicket>(this, (f) => {
                this.Flight = f.SendFlightFromFlightVM;

                if (this.Flight.FlightID > 0)
                {
                    this.NewTicket.FlightID = this.Flight.FlightID;
                    this.NewTicket.Flight = this.Flight;
                    this.ForegroundForUser = "#68a225";
                    this.MessageForUser = "Flight Was Added";
                }
              
            });

        }

        /// <summary>
        /// Receive 'PassengerModel' from SendNewPassengerCommand(Passenger view model)
        /// </summary>
        private void ReceivePassengerFromPassengerVM()
        {
            Messenger.Default.Register<MessagePassengerToNewTicket>(this, (p) => {
                this.Passenger = p.SendPassengerFromPassengerVM;

                if (this.Passenger.PassengerID > 0)
                {
                    this.NewTicket.PassengerID = this.Passenger.PassengerID;
                    this.NewTicket.Passenger = this.Passenger;
                    this.ForegroundForUser = "#68a225";
                    this.MessageForUser = "Passenger Was Added";
                }

            });

        }

        /// <summary>
        /// Receive 'CashierModel' from SendNewCashierCommand(Cashier view model)
        /// </summary>
        private void ReceiveCashierFromCashierVM()
        {
            Messenger.Default.Register<MessageCashierToNewTicket>(this, (p) => {
                this.Cashier = p.SendCashierFromCashierVM;

                if (this.Cashier.CashierID > 0)
                {
                    this.NewTicket.CashierID = this.Cashier.CashierID;
                    this.NewTicket.Cashier = this.Cashier;
                    this.ForegroundForUser = "#68a225";
                    this.MessageForUser = "Cashier Was Added";
                }

            });

        }

        /// <summary>
        /// Receive 'TariffModel' from SendNewTariffCommand(Tarrif(Rate) view model)
        /// </summary>
        private void ReceiveTariffFromRateVM()
        {
            Messenger.Default.Register<MessageTariffToNewTicket>(this, (p) => {
                this.Tariff = p.SendTariffFromTariffVM;

                if (this.Tariff.RateID > 0)
                {
                    this.NewTicket.RateID = this.Tariff.RateID;
                    this.NewTicket.Rate = this.Tariff;
                    this.ForegroundForUser = "#68a225";
                    this.MessageForUser = "Tariff Was Added";
                }

            });

        }

        /// <summary>
        /// Check and calculate the cost.
        /// </summary>
        private void CheckCost()
        {
            if (this.Flight != null && this.Tariff != null)
            {
                decimal cost = AllTicketsModel.CalculateFullCost(this.Flight, this.Tariff);

                if (cost == Decimal.MinusOne)
                {
                    this.MessageForUser = "Error occured... Try again, please...";
                    this.ForegroundForUser = "#ff420e";
                }
                else
                {
                    this.NewTicket.TotalCost = cost;
                    this.FullCost = cost;
                    this.MessageForUser = "Full Cost was Calculated.";
                    this.ForegroundForUser = "#68a225";

                }

            }
            else
            {
                this.MessageForUser = "Add Flight And Tariff, please...";
                this.ForegroundForUser = "#ff420e";
            }
        }


        #endregion
    }
}
