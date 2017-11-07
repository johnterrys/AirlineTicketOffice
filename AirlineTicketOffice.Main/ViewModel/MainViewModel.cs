using AirlineTicketOffice.Main.Services.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using AirlineTicketOffice.Main.Services.Messenger;
using System;
using System.Windows;
using AirlineTicketOffice.Main.Services.Dialog;
using AirlineTicketOffice.Model.IRepository;
using System.Threading.Tasks;
using AirlineTicketOffice.Main.Properties;

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
                             IDialogMessage dialogeMessage,
                             ITicketRepository ticketRepository)
        {
            this.ForegroundForUser = "#ffffff";
            _navigationService = navigationService;
            _dialogMessage = dialogeMessage;
            _ticketRepository = ticketRepository;

            ReceiveStatusFromFlightVM();
        }

        #endregion

        #region fields

        private readonly IMainNavigationService _navigationService;

        private bool connect = true;

        private readonly IDialogMessage _dialogMessage;

        private readonly ITicketRepository _ticketRepository;

        private string _statusWindow;

        private object locker = new object();

        private string _ForegroundForUser;

        #endregion

        #region properties      

        public string StatusWindow
        {
            get { return _statusWindow; }
            set { Set(() => StatusWindow, ref _statusWindow, value); }
        }

        public string ForegroundForUser
        {
            get { return _ForegroundForUser; }
            set { Set(() => ForegroundForUser, ref _ForegroundForUser, value); }
        }

        #endregion

        #region commands


        /// <summary>
        /// Close Main window.
        /// </summary>
        private ICommand _minimizeWindow;

        public ICommand MinimizeWindow
        {
            get
            {
                if (_minimizeWindow == null)
                {
                    _minimizeWindow = new RelayCommand(() =>
                    {
                        if (Application.Current.MainWindow.WindowState != WindowState.Minimized)
                        {
                            Application.Current.MainWindow.WindowState = WindowState.Minimized;
                        }
                    });
                }

                return _minimizeWindow;
            }
        }

        /// <summary>
        /// Close Main window.
        /// </summary>
        private ICommand _closeWindow;

        public ICommand CloseWindow
        {
            get
            {
                if (_closeWindow == null)
                {
                    _closeWindow = new RelayCommand(() =>
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            Application.Current.MainWindow.Close();
                        }
                    });
                }

                return _closeWindow;
            }
        }

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
                        _navigationService.NavigateTo(Resources.NewTicketViewKey);
                        if (this.connect) this.StatusWindow = "New Ticket Window";
                    });
                }
                return _getNewTicketCommand;
            }
            
        }

        /// <summary>
        /// Open message box 'about program'.
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
                            if (this.connect) this.StatusWindow = "Error Dialog About.";
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
                        _navigationService.NavigateTo(Resources.AllPassengerViewKey);
                        if (this.connect) this.StatusWindow = "All Passengers Window";
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
                        _navigationService.NavigateTo(Resources.NewPassengerViewKey);
                        if (this.connect) this.StatusWindow = "New Passenger Window";
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
                        _navigationService.NavigateTo(Resources.BoughtTicketViewKey);
                        if (this.connect) this.StatusWindow = "Purchased Tickets Window";
                    });
                }
                return _getBoughtTicketCommand;
            }

        }


        /// <summary>
        /// Navigate to 'flights' view.
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
                        _navigationService.NavigateTo(Resources.FlightsViewKey);
                        if (this.connect) this.StatusWindow = "Flights Window";
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
                        _navigationService.NavigateTo(Resources.TariffsViewKey);
                        if (this.connect) this.StatusWindow = "Tariffs Window";
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
                        _navigationService.NavigateTo(Resources.CashierViewKey);
                        if (this.connect) this.StatusWindow = "Cashiers Window";
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
            // Set culture:

            //System.Threading.Thread.CurrentThread.CurrentCulture =
            //   System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            //System.Threading.Thread.CurrentThread.CurrentUICulture = 
            //    System.Threading.Thread.CurrentThread.CurrentCulture;

            _navigationService.NavigateTo(Resources.NewTicketViewKey);
            if (this.connect) this.StatusWindow = "New Ticket Window";

            CheckConnectionDB();
        }

        /// <summary>
        /// Receive Status from SendNewTicketCommand(flight view model)
        /// </summary>
        private void ReceiveStatusFromFlightVM()
        {                      
            Messenger.Default.Register<MessageStatus>(this, (f) => {
                if(this.connect) this.StatusWindow = f.MessageStatusFromFlight;
            });

        }

        /// <summary>
        /// Check existing database.
        /// </summary>
        private void CheckConnectionDB()
        {
            Task.Factory.StartNew(() =>
            {
                lock (locker)
                {
                    if (!_ticketRepository.CheckConnection())
                    {

                        Application.Current.Dispatcher.Invoke(
                                new Action(() =>
                                {
                                    this.ForegroundForUser = "#ff420e";
                                    this.StatusWindow = "CAN NOT CONNECT TO DATABASE.";
                                    this.connect = false;
                                }));
                    }                 

                }

            });
        }

        #endregion

    }
}
