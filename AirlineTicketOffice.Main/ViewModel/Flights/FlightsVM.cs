using AirlineTicketOffice.Main.Services.Messenger;
using AirlineTicketOffice.Main.Services.Navigation;
using AirlineTicketOffice.Model.IRepository;
using AirlineTicketOffice.Model.Models;
using AirlineTicketOffice.Repository.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AirlineTicketOffice.Main.ViewModel.Flights
{
    public sealed class FlightsVM: ViewModelBase
    {
       
        #region constructor
        public FlightsVM(IFlightRepository flightRepository,
                         IPlaceInFlightRepository placeFlightRepository,
                         IMainNavigationService navigationService,
                         IPlaceInAircraftRepository placeAirRepository)
        {

            _flightRepository = flightRepository;
            _placeFlightRepository = placeFlightRepository;
            _navigationService = navigationService;
            _placeAirRepository = placeAirRepository;

            this.Flight = new FlightModel();

            this.DateSearch = DateTime.Now;

            Task.Factory.StartNew(() =>
            {
                GetAllFlightLock();

                this.PlaceInFlight = null;

                this.PlaceInAircraft = null;

                Application.Current.Dispatcher.Invoke(
                      new Action(() =>
                      {
                          this.DataGridVisibility = "Collapsed";

                      }));
                
            });

            ReceiveFlight();
        }

      

        #endregion

        #region fields

        private object locker = new object();

        private readonly IFlightRepository _flightRepository;

        private readonly IPlaceInFlightRepository _placeFlightRepository;

        private readonly IMainNavigationService _navigationService;

        private readonly IPlaceInAircraftRepository _placeAirRepository;

        private ObservableCollection<FlightModel> _flights;

        private IEnumerable<PlaceInFlightModel> _placeInFlight;

        private IEnumerable<PlaceInAircraftModel> _placeInAircraft;

        private string _nameRoute;

        private string _flightNumber;

        private FlightModel _flight;

        private string _dataGridVisibility;

        private string _MessageForUser;

        private string _ForegroundForUser;

        private string _BusinessFreeRect;

        private string _BusinessBusyRect;

        private string _BusinessPlaceFree;

        private string _BusinessPlaceBusy;

        private string _EconomFreeRect;

        private string _EconomBusyRect;

        private string _EconomyPlaceFree;

        private string _EconomyPlaceBusy;

        private DateTime _dateSearch;

        #endregion

        #region properties

        public DateTime DateSearch
        {
            get { return _dateSearch; }
            set { Set(() => DateSearch, ref _dateSearch, value); }
        }

        public string BusinessPlaceFree
        {
            get { return _BusinessPlaceFree; }
            set { Set(() => BusinessPlaceFree, ref _BusinessPlaceFree, value); }
        }

        public string BusinessPlaceBusy
        {
            get { return _BusinessPlaceBusy; }
            set { Set(() => BusinessPlaceBusy, ref _BusinessPlaceBusy, value); }
        }

        public string EconomFreeRect
        {
            get { return _EconomFreeRect; }
            set { Set(() => EconomFreeRect, ref _EconomFreeRect, value); }
        }

        public string EconomBusyRect
        {
            get { return _EconomBusyRect; }
            set { Set(() => EconomBusyRect, ref _EconomBusyRect, value); }
        }

        public string EconomyPlaceFree
        {
            get { return _EconomyPlaceFree; }
            set { Set(() => EconomyPlaceFree, ref _EconomyPlaceFree, value); }
        }

        public string EconomyPlaceBusy
        {
            get { return _EconomyPlaceBusy; }
            set { Set(() => EconomyPlaceBusy, ref _EconomyPlaceBusy, value); }
        }

        public string BusinessFreeRect
        {
            get { return _BusinessFreeRect; }
            set { Set(() => BusinessFreeRect, ref _BusinessFreeRect, value); }
        }

        public string BusinessBusyRect
        {
            get { return _BusinessBusyRect; }
            set { Set(() => BusinessBusyRect, ref _BusinessBusyRect, value); }
        }

        public string ForegroundForUser
        {
            get { return _ForegroundForUser; }
            set { Set(() => ForegroundForUser, ref _ForegroundForUser, value); }
        }

        public string DataGridVisibility
        {
            get { return _dataGridVisibility; }
            set { Set(() => DataGridVisibility, ref _dataGridVisibility, value); }
        }

        public FlightModel Flight
        {
            get { return _flight; }
            set { Set(() => Flight, ref _flight, value); }

        }

        public string NameRoute
        {
            get { return _nameRoute; }
            set { Set(() => NameRoute, ref _nameRoute, value); }
        }

        public string MessageForUser
        {
            get { return _MessageForUser; }
            set { Set(() => MessageForUser, ref _MessageForUser, value); }
        }

        public IEnumerable<PlaceInFlightModel> PlaceInFlight
        {
            get { return _placeInFlight; }
            set { Set(() => PlaceInFlight, ref _placeInFlight, value); }
        }

        public IEnumerable<PlaceInAircraftModel> PlaceInAircraft
        {
            get { return _placeInAircraft; }
            set { Set(() => PlaceInAircraft, ref _placeInAircraft, value); }
        }

        public string FlightNumber
        {
            get { return _flightNumber; }
            set { Set(() => FlightNumber, ref _flightNumber, value); }
        }

        public ObservableCollection<FlightModel> Flights
        {
            get { return _flights; }
            set { Set(() => Flights, ref _flights, value); }
        }

        #endregion

        #region commands      

        private ICommand _SendNewTicketCommand;

        /// <summary>
        /// Send this.Flight to NewTicket view model.
        /// </summary>
        public ICommand SendNewTicketCommand
        {
            get
            {
                if (_SendNewTicketCommand == null)
                {
                    _SendNewTicketCommand = new RelayCommand(() =>
                    {
                        if (this.Flight != null)
                        {
                            _navigationService.NavigateTo("NewTicketViewKey", "New Ticket Window");

                            Messenger.Default.Send<MessageFlightToNewTicket>(new MessageFlightToNewTicket()
                            {
                                SendFlightFromFlightVM = this.Flight
                            });

                            Messenger.Default.Send<MessageStatus>(new MessageStatus()
                            {
                                MessageStatusFromFlight = "New Ticket Window"
                            });

                           
                        }                     

                    });
                }
                return _SendNewTicketCommand;
            }
            set { _SendNewTicketCommand = value; }
        }


        private ICommand _getAllFlightCommand;

        /// <summary>
        /// Get all flight in db.
        /// </summary>
        public ICommand GetAllFlightCommand
        {
            get
            {
                if (_getAllFlightCommand == null)
                {
                    _getAllFlightCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (this.Flights != null) this.Flights.Clear();

                            Task.Factory.StartNew(() =>
                            {
                                GetAllFlightLock();
                            });
                          
                        }
                        catch (Exception e)
                        {
                            this.MessageForUser = e.Message;
                        }

                    });
                }
                return _getAllFlightCommand;
            }
            set { _getAllFlightCommand = value; }
        }

        private ICommand _searchDateCommand;

        public ICommand SearchDateCommand
        {
            get
            {
                if (_searchDateCommand == null)
                {
                    _searchDateCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (this.Flights != null) this.Flights.Clear();

                            if (this.FlightNumber == String.Empty)
                            {
                                this.MessageForUser = "Need enter Flight Number:)";
                                return;
                            }

                            this.MessageForUser = this.DateSearch.Date.ToString();

                            var res = from e in _flightRepository.GetAll()
                                      where (e.DateOfDeparture.Date == DateSearch.Date)
                                      select e;

                            foreach (var item in res)
                            {
                                this.Flights.Add(item);
                            }
                        }
                        catch (Exception e)
                        {
                            this.MessageForUser = e.Message;
                        }
                    });
                }
                return _searchDateCommand;
            }
            set { _searchDateCommand = value; }
        }

        private ICommand _searchFlightCommand;


        /// <summary>
        /// Search flights by flight number.
        /// </summary>
        public ICommand SearchFlightCommand
        {
            get
            {
                if (_searchFlightCommand == null)
                {
                    _searchFlightCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (this.Flights != null) this.Flights.Clear();

                            if (this.FlightNumber == String.Empty)
                            {
                                this.MessageForUser = "Need enter Flight Number:)";
                                return;
                            }

                            var res = from e in _flightRepository.GetAll()
                                      where e.FlightNumber.StartsWith(FlightNumber, true, CultureInfo.InvariantCulture)
                                      select e;

                            foreach (var item in res)
                            {
                                this.Flights.Add(item);
                            }
                        }
                        catch (Exception e)
                        {
                            this.MessageForUser = e.Message;
                        }
                    });
                }
                return _searchFlightCommand;
            }
            set { _searchFlightCommand = value; }
        }


        /// <summary>
        ///  Search flight by route name.
        /// </summary>
        private ICommand _searchRouteCommand;

        public ICommand SearchRouteCommand
        {
            get
            {
                if (_searchRouteCommand == null)
                {
                    _searchRouteCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (this.Flights != null) this.Flights.Clear();

                            if (this.NameRoute == String.Empty)
                            {
                                this.MessageForUser = "Need enter Name Route:)";
                                return;
                            }

                            var res = from e in _flightRepository.GetAll()
                                      where e.Route.NameRoute.StartsWith(NameRoute, true, CultureInfo.InvariantCulture)
                                      select e;

                            foreach (var item in res)
                            {
                                this.Flights.Add(item);
                            }
                        }
                        catch (Exception e)
                        {
                            this.MessageForUser = e.Message;
                        }
                    });
                }
                return _searchRouteCommand;
            }
            set { _searchRouteCommand = value; }
        }

        /// <summary>
        /// The method to send the selected flight from the DataGrid on UI
        /// to the View Model
        /// </summary>
        /// <param name="p"></param>
        private ICommand _sendFlightCommand;



        /// <summary>
        /// Send flight data from UI to this.Flfght.
        /// Compute UI Charts.(May be send realisation to PlaceInFlightChart?)           !!!NOTICE
        /// </summary>
        public ICommand SendFlightCommand
        {
            get
            {
                if (_sendFlightCommand == null)
                {
                    _sendFlightCommand = new RelayCommand<FlightModel>((f) =>
                    {
                        if (f != null)
                        {
                            Messenger.Default.Send<MessageFlight>(new MessageFlight()
                            {
                                SendFlight = f
                            });

                            /// <summary>
                            /// Compute UI Charts.(May be send realisation to PlaceInFlightChart?)   !!!NOTICE
                            /// </summary>
                            GetChartPlaces();
                           
                        }
                    });
                }
                return _sendFlightCommand;
            }
            set { _sendFlightCommand = value; }
        }

       

        #endregion


        #region methods
        /// <summary>
        /// Get flights from bd.
        /// </summary>
        private void GetAllFlightLock()
        {
            lock (locker)
            {
                try
                {
                    this.Flights = new ObservableCollection<FlightModel>(_flightRepository.GetAll());
                    this.ForegroundForUser = "#68a225";
                    this.MessageForUser = "Data Load Success.";
                }
                catch (ArgumentNullException ex)
                {
                    Debug.WriteLine("GetAllFlightLock() fail..." + ex.Message);

                    Application.Current.Dispatcher.Invoke(
                     new Action(() =>
                     {
                         this.DataGridVisibility = "Collapsed";
                         this.ForegroundForUser = "#ff420e";
                         this.MessageForUser = "Data Not Load. Click'Refrash Data' to Load Data again.";
                        
                     }));
                }
               
            }
            
        }

        /// <summary>
        /// Receive this.Flight from SendFlightCommand
        /// </summary>
        private void ReceiveFlight()
        {
            if (this.Flight != null)
            {
                Messenger.Default.Register<MessageFlight>(this, (f) => {
                    this.Flight = f.SendFlight;
                });
            }
        }

        /// <summary>
        /// Compute UI Charts.(May be send realisation to PlaceInFlightChart?)           !!!NOTICE
        /// </summary>
        private void GetChartPlaces()
        {
            // PlaceInFlightModel for 'business rate'
            PlaceInFlightModel businessFreeRectModel = null;
            // PlaceInFlightModel for 'econom rate'
            PlaceInFlightModel economFreeRectModel = null;

            // PlaceInAircraftModel for 'business rate'
            PlaceInAircraftModel businessAllPlace = null;
            // PlaceInAircraftModel for 'econom rate'
            PlaceInAircraftModel economAllPlace = null;


            try
            {

                // get all free places in flight:
                this.PlaceInFlight = _placeFlightRepository.GetPlacesOnFlight(this.Flight.FlightID);

                // get all places in aircraft:
                this.PlaceInAircraft = _placeAirRepository.GetPlacesOnAircraft(this.Flight.AircraftID);

                if (this.PlaceInFlight != null || this.PlaceInAircraft != null)
                {

                    businessFreeRectModel = this.PlaceInFlight.Where(p => p.TypePlace == "A").SingleOrDefault();
                    economFreeRectModel = this.PlaceInFlight.Where(p => p.TypePlace == "B").SingleOrDefault();

                    businessAllPlace = this.PlaceInAircraft.Where(p => p.TypePlace == "A").SingleOrDefault();
                    economAllPlace = this.PlaceInAircraft.Where(p => p.TypePlace == "B").SingleOrDefault();

                    ////////////////////////////////////////  Economy rate chart /////////////////////////////////////

                                                       /* !!! Means 1 place == 5px. !!! */

                    // get 'Economy rate' free Rect 
                    this.EconomFreeRect = "0,40 " + (CheckResult(economAllPlace.Amount * 5)).ToString() + ",40";

                    // get 'Economy rate' Busy Rect
                    this.EconomBusyRect = "0,40 " + (CheckResult((economAllPlace.Amount - economFreeRectModel.Amount)
                              * 5)).ToString() + ",40";

                    // Show Busy econom place in Flight:
                    this.EconomyPlaceBusy = (CheckResult(economAllPlace.Amount - economFreeRectModel.Amount)).ToString();

                    // Show Free econom place in Flight:
                    this.EconomyPlaceFree = economFreeRectModel.Amount.ToString();

                    /////////////////////////////////////////////////////////////////////////////////////////////////             

                    ////////////////////////////////////////  Business rate chart ///////////////////////////////////

                    // get 'Business rate' free Rect 
                    this.BusinessFreeRect = "0,120 " + (CheckResult(businessAllPlace.Amount * 5)).ToString() + ",40";

                    // get 'Business rate' Busy Rect
                    this.BusinessBusyRect = "0,120 " + (CheckResult((businessAllPlace.Amount - businessFreeRectModel.Amount)
                              * 5)).ToString() + ",40";

                    // Show Busy business place in Flight:
                    this.BusinessPlaceBusy = (CheckResult(businessAllPlace.Amount - businessFreeRectModel.Amount)).ToString();

                    // Show Free business place in Flight:
                    this.BusinessPlaceFree = businessFreeRectModel.Amount.ToString();
                    /////////////////////////////////////////////////////////////////////////////////////////////////



                    // Message in TextBlock:
                    this.ForegroundForUser = "#68a225";
                    this.MessageForUser = "Business: " + businessFreeRectModel.Amount.ToString() +
                                           " | Econom: " + economFreeRectModel.Amount.ToString();


                }
                else
                {
                    this.MessageForUser = "Error occured... Try again, please...";
                    this.ForegroundForUser = "#ff420e";
                }
            }
            catch (ArithmeticException ex)
            {
                this.MessageForUser = "Error occured... Try again, please...";
                this.ForegroundForUser = "#ff420e";
                Debug.WriteLine("'_sendFlightCommand' method fail..." + ex.Message);
            }
            catch (Exception ex)
            {
                this.MessageForUser = "Error occured... Try again, please...";
                this.ForegroundForUser = "#ff420e";
                Debug.WriteLine("'_sendFlightCommand' method fail..." + ex.Message);
            }
        }

        ///* Check result operation. */
        private double CheckResult(double d)
        {
            if (double.IsNegativeInfinity(d) || double.IsPositiveInfinity(d) || double.IsNaN(d))
            {
                throw new ArithmeticException();
            }
            else
                return d;
        }

        #endregion

    }

}