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
using System.Globalization;
using GalaSoft.MvvmLight.Views;

namespace AirlineTicketOffice.Main.ViewModel.Passengers
{
    public sealed class AllPassengersVM : ViewModelBase
    {
        #region constructor
        public AllPassengersVM(IPassengerRepository repository,
                               IMainNavigationService navigationService)
        {

            this.DataGridVisibility = true;
            this.ButtonVisible = false;

            _repository = repository;
            _navigationService = navigationService;

            Task.Factory.StartNew(() =>
            {
                lock(locker)
                {
                    this.Passengers = new ObservableCollection<PassengerModel>(_repository.GetAll());
                } 

                Application.Current.Dispatcher.Invoke(
                      new Action(() =>
                      {
                          this.DataGridVisibility = false;
                          this.ButtonVisible = true;
                          this.ForegroundForUser = "#f2f2f2";
                          this.MessageForUser = "Please, Enter A Data...";

                      }));
            });

            ReceivePassenger();
        }
        #endregion

        #region fields

        private readonly IPassengerRepository _repository;

        private readonly IMainNavigationService _navigationService;

        private ObservableCollection<PassengerModel> _passengers;

        private PassengerModel _passenger;

        private bool _dataGridVisibility;

        private bool _ButtonVisible;

        private string _ForegroundForUser;

        private string _MessageForUser;

        object locker = new object();

        private string _passportNumber;

        #endregion

        #region properties

        public string PassportNumber
        {
            get { return _passportNumber; }
            set { Set(() => PassportNumber, ref _passportNumber, value); }
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

        public PassengerModel Passenger
        {
            get { return _passenger; }
            set { Set(() => Passenger, ref _passenger, value); }

        }      

        public bool ButtonVisible
        {
            get { return _ButtonVisible; }
            set { Set(() => ButtonVisible, ref _ButtonVisible, value); }
        }

        public bool DataGridVisibility
        {
            get { return _dataGridVisibility; }
            set { Set(() => DataGridVisibility, ref _dataGridVisibility, value); }
        }


        public ObservableCollection<PassengerModel> Passengers
        {
            get { return _passengers; }
            set { Set(() => Passengers, ref _passengers, value); }
        }

        #endregion

        #region commands      

        private ICommand _getAllPassengerCommand;


        public ICommand GetAllPassengerCommand
        {
            get
            {
                if (_getAllPassengerCommand == null)
                {
                    _getAllPassengerCommand = new RelayCommand(() =>
                    {
                        Task.Factory.StartNew(() =>
                        {
                            this.Passengers = new ObservableCollection<PassengerModel>(_repository.GetAll());
                        });

                    });
                }
                return _getAllPassengerCommand;
            }
            set { _getAllPassengerCommand = value; }
        }


        /// <summary>
        /// The method to send the selected Passenger from the DataGrid on UI
        /// to the View Model
        /// </summary>
        /// <param name="p"></param>
        private ICommand _sendPassengerCommand;

        public ICommand SendPassengerCommand
        {
            get
            {
                if (_sendPassengerCommand == null)
                {
                    _sendPassengerCommand = new RelayCommand<PassengerModel>((p) =>
                    {
                        if (p != null)
                        {
                            Messenger.Default.Send<MessageSendPassenger>(new MessageSendPassenger()
                            {
                                SendPassenger = p
                            });
                        }
                    });
                }
                return _sendPassengerCommand;
            }
            set { _sendPassengerCommand = value; }
        }


        /// <summary>
        /// Update passenger in db via repository.
        /// </summary>
        private ICommand _savePassengerChangeCommand;

        public ICommand SavePassengerChangeCommand
        {
            get
            {
                if (_savePassengerChangeCommand == null)
                {
                    _savePassengerChangeCommand = new RelayCommand<PassengerModel>((p) =>
                    {

                        try
                        {
                            if (_repository.Update(p))
                            {
                                RaisePropertyChanged("Passenger");
                                this.MessageForUser = "Changing of data has passed successfully.";
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
                            Debug.WriteLine("'SavePassengerChangeCommand' method fail..." + ex.Message);
                        }

                    });
                }
                return _savePassengerChangeCommand;
            }
            set { _savePassengerChangeCommand = value; }
        }

        private ICommand _searchPassengerCommand;

        public ICommand SearchPassengerCommand
        {
            get
            {
                if (_searchPassengerCommand == null)
                {
                    _searchPassengerCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (this.Passengers != null) this.Passengers.Clear();

                            if (this.PassportNumber == String.Empty)
                            {
                                this.MessageForUser = "Need Enter Passport Of Number:)";
                                return;
                            }

                            var res = from e in _repository.GetAll()
                                      where e.PassportNumber.StartsWith(PassportNumber, true, CultureInfo.InvariantCulture)
                                      select e;

                            foreach (var item in res)
                            {
                                this.Passengers.Add(item);
                            }
                        }
                        catch (Exception e)
                        {
                            this.MessageForUser = e.Message;
                        }
                    });
                }
                return _searchPassengerCommand;
            }
            set { _searchPassengerCommand = value; }
        }

        private ICommand _sendPassengerToTicketCommand;

        /// <summary>
        /// Send this.Passenger to NewTicket view model.
        /// </summary>
        public ICommand SendPassengerToTicketCommand
        {
            get
            {
                if (_sendPassengerToTicketCommand == null)
                {
                    _sendPassengerToTicketCommand = new RelayCommand(() =>
                    {
                        if (this.Passenger != null)
                        {                           

                            Messenger.Default.Send<MessagePassengerToNewTicket>(new MessagePassengerToNewTicket()
                            {
                                SendPassengerFromPassengerVM = this.Passenger
                            });

                            Messenger.Default.Send<MessageStatus>(new MessageStatus()
                            {
                                MessageStatusFromFlight = "New Ticket Window"
                            });

                            _navigationService.NavigateTo("NewTicketViewKey", "New Ticket Window");

                        }

                    });
                }
                return _sendPassengerToTicketCommand;
            }
            set { _sendPassengerToTicketCommand = value; }
        }


        #endregion

        #region methods

        /// <summary>
        /// The Method used to Receive the send Passenger from the DataGrid UI
        /// and assigning it the the passenger Notifiable property so that
        /// it will be displayed on the other view.
        /// </summary>
        void ReceivePassenger()
        {           
            Messenger.Default.Register<MessageSendPassenger>(this, (p) => {
                this.Passenger = p.SendPassenger;
            });
            
        }

        #endregion
    }
}
