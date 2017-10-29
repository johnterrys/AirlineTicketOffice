using AirlineTicketOffice.Main.Services.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using AirlineTicketOffice.Main.Services.Messenger;
using System.Diagnostics;
using System;
using System.Windows;
using System.Reflection;
using AirlineTicketOffice.Main.Services.Dialog;

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
        public MainViewModel(IMainNavigationService navigationService,
                             IDialogMessage dialogeMessage)
        {
            _navigationService = navigationService;
            _dialogMessage = dialogeMessage;

            ReceiveStatusFromFlightVM();
        }

        #endregion

        #region fields

        private readonly IMainNavigationService _navigationService;

        private readonly IDialogMessage _dialogMessage;

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

        /// <summary>
        /// Navigate to 'NewTicket' view.
        /// </summary>
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

        /// <summary>
        /// Navigate to 'NewTicket' view.
        /// </summary>
        private ICommand _helpCommand;

        public ICommand HelpCommand
        {
            get
            {
                if (_helpCommand == null)
                {
                    _helpCommand = new RelayCommand(() =>
                    {
                        if (_dialogMessage.Show() == false)
                        {
                            this.StatusWindow = "Error Dialog About.";
                        }
                        
                    });
                }
                return _helpCommand;
            }

        }


        /// <summary>
        /// Navigate to 'AllPassengers' view.
        /// </summary>
        private ICommand _getAllPassengerCommand;

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


        /// <summary>
        /// Navigate to 'NewPassenger' view.
        /// </summary>
        private ICommand _getNewPassengerCommand;

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

        /// <summary>
        /// Navigate to 'purchased tickets' view.
        /// </summary>
        private ICommand _getBoughtTicketCommand;

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


        /// <summary>
        /// Navigate to 'Cashiers' view.
        /// </summary>
        private ICommand _getFlightsCommand;

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


        /// <summary>
        /// Navigate to 'Tariffs' view.
        /// </summary>
        private ICommand _getTariffsCommand;

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


        /// <summary>
        /// Navigate to 'Cashiers' view.
        /// </summary>
        private ICommand _getCashierCommand;
  
        public ICommand GetCashierCommand
        {
            get
            {
                if (_getCashierCommand == null)
                {
                    _getCashierCommand = new RelayCommand(() =>
                    {
                        _navigationService.NavigateTo("CashierViewKey");
                        this.StatusWindow = "Cashiers Window";
                    });
                }
                return _getCashierCommand;
            }
        }


        /// <summary>
        /// Occur when the window is loaded.
        /// </summary>
        private ICommand _loadedCommand;
 
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


        /// <summary>
        /// Occur when the window is closed.
        /// </summary>
        public ICommand _closingCommand;

        public ICommand ClosingCommand
        {
            get
            {
                if (_closingCommand == null)
                {
                    _closingCommand = new RelayCommand(() =>
                    {
                        // some work doing...(I did not invented yet)
                    });
                }
                return _closingCommand;
            }

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

            _navigationService.NavigateTo("NewTicketViewKey");
            this.StatusWindow = "New Ticket Window";
        }

        /// <summary>
        /// Receive Status from SendNewTicketCommand(flight view model)
        /// </summary>
        private void ReceiveStatusFromFlightVM()
        {                      
            Messenger.Default.Register<MessageStatus>(this, (f) => {
                this.StatusWindow = f.MessageStatusFromFlight;
            });

        }

        #endregion

    }
}