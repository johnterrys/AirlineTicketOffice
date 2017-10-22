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

namespace AirlineTicketOffice.Main.ViewModel.Passengers
{
    public sealed class AllPassengersVM : ViewModelBase
    {
        #region constructor
        public AllPassengersVM(IPassengerRepository repository)
        {
            _repository = repository;

            this.Passenger = new PassengerModel();

            this.ButtonLoadVisible = "Hidden";

            Task.Factory.StartNew(() =>
            {
                lock(locker)
                {
                    this.Passengers = new ObservableCollection<PassengerModel>(_repository.GetAll());
                } 

                Application.Current.Dispatcher.Invoke(
                      new Action(() =>
                      {
                          this.DataGridVisibility = "Collapsed";
                          this.ButtonLoadVisible = "Visible";
                          this.ForegroundForUser = "#f2f2f2";
                          this.MessageForUser = "Please, Enter A Data...";

                      }));
            });

            ReceivePassenger();
        }
        #endregion

        #region fields

        private readonly IPassengerRepository _repository;

        private ObservableCollection<PassengerModel> _passengers;

        private PassengerModel _passenger;

        private string _dataGridVisibility;

        private string _ButtonLoadVisible;

        private string _ForegroundForUser;

        private string _MessageForUser;

        object locker = new object();

        #endregion

        #region properties

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

        public string ButtonLoadVisible
        {
            get { return _ButtonLoadVisible; }
            set { Set(() => ButtonLoadVisible, ref _ButtonLoadVisible, value); }
        }

        public string DataGridVisibility
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

        #endregion

        #region methods

        /// <summary>
        /// The Method used to Receive the send Passenger from the DataGrid UI
        /// and assigning it the the passenger Notifiable property so that
        /// it will be displayed on the other view.
        /// </summary>
        void ReceivePassenger()
        {
            if (this.Passenger != null)
            {
                Messenger.Default.Register<MessageSendPassenger>(this, (p) => {
                    this.Passenger = p.SendPassenger;
                });
            }
        }

        #endregion
    }
}
