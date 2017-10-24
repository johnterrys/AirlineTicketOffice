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

            Task.Factory.StartNew(() =>
            {

                Application.Current.Dispatcher.Invoke(
                      new Action(() =>
                      {
                          
                          this.DataGridVisibility = "Collapsed";
                          this.ButtonLoadVisible = "Visible";
                          this.ForegroundForUser = "#f2f2f2";
                          this.MessageForUser = "At First You Need Select The Flight.";

                      }));
            });

            ReceiveFlightFromFlightVM();
            ReceivePassengerFromFlightVM();
            ReceiveCashierFromFlightVM();
            ReceiveTariffFromFlightVM();

        }

      
        #endregion

        #region fields

        object locker = new object();

        private readonly ITicketRepository _ticketRepository;        

        private AllTicketsModel _newTicket;

        private string _ForegroundForUser;

        private string _MessageForUser;

        private string _dataGridVisibility;

        private string _ButtonLoadVisible;

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

        public string DataGridVisibility
        {
            get { return _dataGridVisibility; }
            set { Set(() => DataGridVisibility, ref _dataGridVisibility, value); }
        }

        public string ButtonLoadVisible
        {
            get { return _ButtonLoadVisible; }
            set { Set(() => ButtonLoadVisible, ref _ButtonLoadVisible, value); }
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

        private ICommand _routeCostCommand;

        public ICommand RouteCostCommand
        {
            get
            {
                if (_routeCostCommand == null)
                {
                    _routeCostCommand = new RelayCommand(() =>
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
                                this.FullCost = cost;
                                this.MessageForUser = "Full Cost was Calculated.";
                                this.ForegroundForUser = "#68a225";

                            }
                            
                        }
                        
                    });
                }
                return _routeCostCommand;
            }

            set { _routeCostCommand = value; }
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
                    _saveNewTicketCommand = new RelayCommand<AllTicketsModel>((t) =>
                    {

                        try
                        {
                            if (_ticketRepository.Add(t))
                            {
                                RaisePropertyChanged("NewTicket");
                                this.NewTicket = new AllTicketsModel();
                                this.MessageForUser = "Inserting of data has passed successfully..";
                                this.ForegroundForUser = "#68a225";
                            }
                            else
                            {
                                this.MessageForUser = "Inserting Data Is Not Passed.";
                                this.ForegroundForUser = "#ff420e";
                            }
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
                    this.ForegroundForUser = "#33cc66";
                    this.MessageForUser = "Flight Was Added";
                }
              
            });

        }

        /// <summary>
        /// Receive 'PassengerModel' from SendNewPassengerCommand(Passenger view model)
        /// </summary>
        private void ReceivePassengerFromFlightVM()
        {
            Messenger.Default.Register<MessagePassengerToNewTicket>(this, (p) => {
                this.Passenger = p.SendPassengerFromPassengerVM;

                if (this.Passenger.PassengerID > 0)
                {
                    this.ForegroundForUser = "#33cc66";
                    this.MessageForUser = "Passenger Was Added";
                }

            });

        }

        /// <summary>
        /// Receive 'CashierModel' from SendNewCashierCommand(Cashier view model)
        /// </summary>
        private void ReceiveCashierFromFlightVM()
        {
            Messenger.Default.Register<MessageCashierToNewTicket>(this, (p) => {
                this.Cashier = p.SendCashierFromCashierVM;

                if (this.Cashier.CashierID > 0)
                {
                    this.ForegroundForUser = "#33cc66";
                    this.MessageForUser = "Cashier Was Added";
                }

            });

        }

        /// <summary>
        /// Receive 'TariffModel' from SendNewTariffCommand(Tarrif(Rate) view model)
        /// </summary>
        private void ReceiveTariffFromFlightVM()
        {
            Messenger.Default.Register<MessageTariffToNewTicket>(this, (p) => {
                this.Tariff = p.SendTariffFromTariffVM;

                if (this.Tariff.RateID > 0)
                {
                    this.ForegroundForUser = "#33cc66";
                    this.MessageForUser = "Tariff Was Added";
                }

            });

        }

        #endregion
    }
}
