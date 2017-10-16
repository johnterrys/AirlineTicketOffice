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
                         IPlaceInFlightRepository placeRepository,
                         IMainNavigationService navigationService,
                         IPlaceInAircraftRepository placeAirRepository)
        {

            _flightRepository = flightRepository;
            _placeRepository = placeRepository;
            _navigationService = navigationService;
            _placeAirRepository = placeAirRepository;

            this.Flight = new FlightModel();

            Task.Factory.StartNew(() =>
            {
                GetAllFlightLock();

                this.PlaceOnFlight = null;

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

        private readonly IPlaceInFlightRepository _placeRepository;

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

        #endregion

        #region properties

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

        public IEnumerable<PlaceInFlightModel> PlaceOnFlight
        {
            get { return _placeInFlight; }
            set { Set(() => PlaceOnFlight, ref _placeInFlight, value); }
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
                            this.Flights?.Clear();

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
                            this.Flights?.Clear();

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
                            this.Flights?.Clear();

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
        /// The method to send the selected Passenger from the DataGrid on UI
        /// to the View Model
        /// </summary>
        /// <param name="p"></param>
        private ICommand _sendFlightCommand;

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
                            Messenger.Default.Send<MessageCommunicator>(new MessageCommunicator()
                            {
                                SendFlight = f
                            });

                            try
                            {
                                this.PlaceOnFlight = _placeRepository.GetPlacesOnFlight(this.Flight.FlightID);
                                this.PlaceInAircraft = _placeAirRepository.GetPlacesOnAircraft(this.Flight.FlightID);

                                if (this.PlaceOnFlight != null)
                                {
                                    PlaceInFlightModel businessFreeRectModel = this.PlaceOnFlight.Where(p => p.TypePlace == "A").SingleOrDefault();
                                    PlaceInFlightModel economFreeRectModel = this.PlaceOnFlight.Where(p => p.TypePlace == "B").SingleOrDefault();

                                    PlaceInAircraftModel businessAllPlace = this.PlaceInAircraft.Where(p => p.TypePlace == "A").SingleOrDefault();
                                    PlaceInAircraftModel economAllPlace = this.PlaceInAircraft.Where(p => p.TypePlace == "B").SingleOrDefault();

                                    this.ForegroundForUser = "#68a225";
                                    this.BusinessFreeRect = "0,40 " + ((900 / 100) * (businessAllPlace.Amount * 3)).ToString()  + ",40";
                                    this.BusinessBusyRect = "0,40 " + ((900 / 100) * (businessAllPlace.Amount * 3) - businessFreeRectModel.Amount).ToString() + ",40";
                                    this.BusinessPlaceFree = businessFreeRectModel.Amount.ToString();
                                    this.EconomyPlaceFree = economFreeRectModel.Amount.ToString();
                                    this.BusinessPlaceBusy = (businessAllPlace.Amount - businessFreeRectModel.Amount).ToString();
                                    this.EconomBusyRect = (economAllPlace.Amount - economFreeRectModel.Amount).ToString();
                                    this.MessageForUser = "Business: " + businessFreeRectModel.Amount.ToString() + 
                                                           " | Econom: " + economFreeRectModel.Amount.ToString();
                                }
                                else
                                {
                                    this.MessageForUser = "Error occured... Try again, please...";
                                this.ForegroundForUser = "#ff420e";
                                }
                            }
                            catch (Exception ex)
                            {
                                this.MessageForUser = "Error occured... Try again, please...";
                                this.ForegroundForUser = "#ff420e";
                                Debug.WriteLine("'_sendFlightCommand' method fail..." + ex.Message);
                            }
                           
                        }
                    });
                }
                return _sendFlightCommand;
            }
            set { _sendFlightCommand = value; }
        }


        #endregion


        #region methods

        private void GetAllFlightLock()
        {
            lock (locker)
            {
                this.Flights = new ObservableCollection<FlightModel>(_flightRepository.GetAll());
            }
            
        }

        private void ReceiveFlight()
        {
            if (this.Flight != null)
            {
                Messenger.Default.Register<MessageCommunicator>(this, (f) => {
                    this.Flight = f.SendFlight;
                });
            }
        }


        #endregion

    }

}