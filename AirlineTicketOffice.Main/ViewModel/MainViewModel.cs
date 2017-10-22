using AirlineTicketOffice.Main.Services.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using AirlineTicketOffice.Main.Properties;
using System.Threading.Tasks;
using System.Windows.Threading;
using System;
using GalaSoft.MvvmLight.Threading;
using System.Threading;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using AirlineTicketOffice.Model.Models;
using AirlineTicketOffice.Main.Services.Messenger;

namespace AirlineTicketOffice.Main.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        #region constructor

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMainNavigationService navigationService)
        {
            _navigationService = navigationService;
            this.StatusWindow = "Main Window";

            ReceiveStatusFromFlightVM();
        }

        #endregion

        #region fields

        private readonly IMainNavigationService _navigationService;

        private string _statusWindow;


        #endregion

        #region properties

        public string StatusWindow
        {
            get { return _statusWindow; }
            set { Set(() => StatusWindow, ref _statusWindow, value); }
        }

        #endregion

        #region commands

        private ICommand _getBoughtTicketCommand;

        private ICommand _getAllPassengerCommand;

        private ICommand _getFlightsCommand;

        private ICommand _getTariffsCommand; 

        private ICommand _loadedCommand;

        private ICommand _getNewPassengerCommand;

        private ICommand _getNewTicketCommand;

        public ICommand GetNewTicketCommand
        {
            get
            {
                if (_getNewTicketCommand == null)
                {
                    _getNewTicketCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("NewTicketViewKey");
                        this.StatusWindow = "New Ticket Window";
                    });
                }
                return _getNewTicketCommand;
            }
            
        }


        public ICommand GetAllPassengerCommand
        {
            get
            {
                if (_getAllPassengerCommand == null)
                {
                    _getAllPassengerCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("AllPassengerViewKey");
                        this.StatusWindow = "All Passengers Window";
                    });
                }
                return _getAllPassengerCommand;
            }
            
        }

        public ICommand GetNewPassengerCommand
        {
            get
            {
                if (_getNewPassengerCommand == null)
                {
                    _getNewPassengerCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("NewPassengerViewKey");
                        this.StatusWindow = "New Passenger Window";
                    });
                }
                return _getNewPassengerCommand;
            }

        }

        public ICommand GetBoughtTicketCommand
        {
            get
            {
                if (_getBoughtTicketCommand == null)
                {
                    _getBoughtTicketCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("BoughtTicketViewKey");
                        this.StatusWindow = "Purchased Tickets Window";
                    });
                }
                return _getBoughtTicketCommand;
            }

        }      

        public ICommand GetFlightsCommand
        {
            get
            {
                if (_getFlightsCommand == null)
                {
                    _getFlightsCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("FlightsViewKey");
                        this.StatusWindow = "Flights Window";
                    });
                }
                return _getFlightsCommand;
            }

        }
      
        public ICommand GetTariffsCommand
        {
            get
            {
                if (_getTariffsCommand == null)
                {
                    _getTariffsCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("TariffsViewKey");
                        this.StatusWindow = "Tariffs Window";
                    });
                }
                return _getTariffsCommand;
            }
        }

        public ICommand LoadedCommand
        {
            get
            {
                if (_loadedCommand == null)
                {
                    _loadedCommand = new RelayCommand(() =>
                    {
                        BeginLoadingMainWindow();

                    });
                }
                return _loadedCommand;
            }
            set { _loadedCommand = value; }

        }

        #endregion

        #region methods

        /// <summary>
        /// Localization.
        /// </summary>
        private void BeginLoadingMainWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
               System.Globalization.CultureInfo.CreateSpecificCulture("ru-Ru");

            System.Threading.Thread.CurrentThread.CurrentUICulture = 
                System.Threading.Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// Receive Status from SendNewTicketCommand(flight view model)
        /// </summary>
        private void ReceiveStatusFromFlightVM()
        {                      
            Messenger.Default.Register<MessageCommunicator>(this, (f) => {
                this.StatusWindow = f.MessageStatusFromFlight;
            });

        }

        #endregion

    }
}