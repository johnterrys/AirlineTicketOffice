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
        public NewTicketVM(ITicketRepository ticketRepository,
                           IFlightRepository flightRepository,
                           IPassengerRepository passengerRepository,
                           ICashierRepository cashierRepository,
                           ITariffsRepository tariffRepository)
        {
            _ticketRepository = ticketRepository;
            _flightRepository = flightRepository;
            _passengerRepository = passengerRepository;
            _cashierRepository = cashierRepository;
            _tariffRepository = tariffRepository;

            this.NewTicket = new AllTicketsModel();

            this.Flight = new FlightModel(); 

            Task.Factory.StartNew(() =>
            {
                lock (locker)
                {
                    //this.AllPassenger = new ObservableCollection<PassengerModel>(_passengerRepository.GetAll());

                    //this.AllFlight = new ObservableCollection<FlightModel>(_flightRepository.GetAll());

                   // this.AllCashier = new ObservableCollection<CashierModel>(_cashierRepository.GetAll());

                   // this._AllTariff = new ObservableCollection<TariffModel>(_tariffRepository.GetAll());
                }

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

        }
        #endregion

        #region fields

        object locker = new object();

        private readonly ITicketRepository _ticketRepository;

        private readonly IFlightRepository _flightRepository;

        private readonly IPassengerRepository _passengerRepository;

        private readonly ICashierRepository _cashierRepository;

        private readonly ITariffsRepository _tariffRepository;

        private AllTicketsModel _newTicket;

        private string _ForegroundForUser;

        private string _MessageForUser;

        private ObservableCollection<FlightModel> _AllFlight;

        private ObservableCollection<PassengerModel> _AllPassenger;

        private ObservableCollection<CashierModel> _AllCashier;

        private ObservableCollection<TariffModel> _AllTariff;

        private string _dataGridVisibility;

        private string _ButtonLoadVisible;

        private FlightModel _flight;

        private PassengerModel _passenger;

        #endregion

        #region properties

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

        public ObservableCollection<FlightModel> AllFlight
        { 
            get { return _AllFlight; }
            set { Set(() => AllFlight, ref _AllFlight, value); }
        }

        public ObservableCollection<PassengerModel> AllPassenger
        {
            get { return _AllPassenger; }
            set { Set(() => AllPassenger, ref _AllPassenger, value); }
        }

        public ObservableCollection<CashierModel> AllCashier
        {
            get { return _AllCashier; }
            set { Set(() => AllCashier, ref _AllCashier, value); }
        }

        public ObservableCollection<TariffModel> AllTariff
        {
            get { return _AllTariff; }
            set { Set(() => AllTariff, ref _AllTariff, value); }
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
                    this.MessageForUser = this.Flight.FlightNumber.ToString();
                }
              
            });

        }

        /// <summary>
        /// Receive 'PassengerModel' from SendNewTicketCommand(Passenger view model)
        /// </summary>
        private void ReceivePassengerFromFlightVM()
        {
            Messenger.Default.Register<MessagePassengerToNewTicket>(this, (p) => {
                this.Passenger = p.SendPassengerFromPassengerVM;

                if (this.Passenger.PassengerID > 0)
                {
                    this.ForegroundForUser = "#33cc66";
                    this.MessageForUser += "|"+ this.Passenger.PassportNumber.ToString();
                }

            });

        }

        #endregion
    }
}
