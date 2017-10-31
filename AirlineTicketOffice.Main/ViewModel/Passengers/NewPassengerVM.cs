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
using AirlineTicketOffice.Main.Services.Dialog;

namespace AirlineTicketOffice.Main.ViewModel.Passengers
{
    public sealed class NewPassengerVM: ViewModelBase
    {
        #region constructor
        public NewPassengerVM(IPassengerRepository repository,
                              IXmlDialogService xmlDialogService)
        {
            _repository = repository;
            _xmlDialogService = xmlDialogService;
          
            this.ForegroundForUser = "#f2f2f2";
            this.MessageForUser = "At First Create 'Blank' For New Passenger";

 
        }
        #endregion

        #region fields

        private readonly IPassengerRepository _repository;

        private readonly IXmlDialogService _xmlDialogService;

        private PassengerModel _passenger;

        private string _ForegroundForUser;

        private string _MessageForUser;


        #endregion

        #region properties

        public PassengerModel Passenger
        {
            get { return _passenger; }
            set { Set(() => Passenger, ref _passenger, value); }

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
        /// Create new passenger in UI.
        /// </summary>
        private ICommand _NewPassengerCommand;

        public ICommand NewPassengerCommand
        {
            get
            {
                if (_NewPassengerCommand == null)
                {
                    _NewPassengerCommand = new RelayCommand<PassengerModel>((p) =>
                    {

                        try
                        {
                            this.Passenger = new PassengerModel();
                            this.Passenger.DateOfBirth = DateTime.MinValue;
                            this.Passenger.TermOfPassportDate = DateTime.MinValue;

                            this.MessageForUser = "New Passenger is Created.";
                            this.ForegroundForUser = "#68a225";

                        }
                        catch (Exception ex)
                        {
                            this.MessageForUser = "New Passenger is NOT Created.";
                            this.ForegroundForUser = "#ff420e";
                            Debug.WriteLine("'SaveNewPassengerCommand' method fail..." + ex.Message);
                        }

                    });
                }
                return _NewPassengerCommand;
            }
        }

        /// <summary>
        /// Create new passenger in db via repository.
        /// </summary>
        private ICommand _saveNewPassengerCommand;

        public ICommand SaveNewPassengerCommand
        {
            get
            {
                if (_saveNewPassengerCommand == null)
                {
                    _saveNewPassengerCommand = new RelayCommand<PassengerModel>((p) =>
                    {

                        try
                        {
                            if (this.Passenger == null)
                            {
                                this.ForegroundForUser = "#f2f2f2";
                                this.MessageForUser = "Please, Enter A Data...";
                                return;
                            }
 
                            if (_repository.Add(p))
                            {
                                RaisePropertyChanged("Passenger");
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
                            Debug.WriteLine("'SaveNewPassengerCommand' method fail..." + ex.Message);
                        }

                    });
                }
                return _saveNewPassengerCommand;
            }
            set { _saveNewPassengerCommand = value; }
        }

        /// <summary>
        /// Open xml File with Passenger Data.
        /// </summary>
        private ICommand _openXmlPassengerCommand;

        public ICommand OpenXmlPassengerCommand
        {
            get
            {
                if (_openXmlPassengerCommand == null)
                {
                    _openXmlPassengerCommand = new RelayCommand<PassengerModel>((p) =>
                    {

                        try
                        {
                            if (_xmlDialogService.OpenFileDialog() == true)
                            {
                                this.Passenger = _xmlDialogService.PassengerFromXml;

                                this.MessageForUser = "File Open Success.";
                                this.ForegroundForUser = "#68a225";
                            }
                            else
                            {
                                this.MessageForUser = "Open File Is Not Passed.";
                                this.ForegroundForUser = "#ff420e";
                            }
                        }
                        catch (Exception ex)
                        {
                            this.MessageForUser = "Open File Is Not Passed.";
                            this.ForegroundForUser = "#ff420e";
                            Debug.WriteLine("'_openXmlPassengerCommand' method fail..." + ex.Message);
                        }

                    });
                }
                return _openXmlPassengerCommand;
            }
            set { _openXmlPassengerCommand = value; }
        }

        /// <summary>
        /// Save xml File with Passenger Data.
        /// </summary>
        private ICommand _saveXmlPassengerCommand;

        public ICommand SaveXmlPassengerCommand
        {
            get
            {
                if (_saveXmlPassengerCommand == null)
                {
                    _saveXmlPassengerCommand = new RelayCommand<PassengerModel>((p) =>
                    {

                        try
                        {
                            if (_xmlDialogService.SavePassenger(this.Passenger) == true)
                            {                              
                                this.MessageForUser = "File Saved Success.";
                                this.ForegroundForUser = "#68a225";
                            }
                            else
                            {
                                this.MessageForUser = "Saving File Is Not Passed.";
                                this.ForegroundForUser = "#ff420e";
                            }
                        }
                        catch (Exception ex)
                        {
                            this.MessageForUser = "Saving File Is Not Passed.";
                            this.ForegroundForUser = "#ff420e";
                            Debug.WriteLine("'_saveXmlPassengerCommand' method fail..." + ex.Message);
                        }

                    });
                }
                return _saveXmlPassengerCommand;
            }
            set { _saveXmlPassengerCommand = value; }
        }

        #endregion

        #region methods

        /// <summary>
        /// The Method used to Receive the send Passenger from the DataGrid UI
        /// and assigning it the the passenger Notifiable property so that
        /// it will be displayed on the other view.
        /// </summary>
        //void ReceivePassenger()
        //{
        //    if (this.Passenger != null)
        //    {
        //        Messenger.Default.Register<MessageCommunicator>(this, (p) => {
        //            this.Passenger = p.SendPassenger;
        //        });
        //    }
        //}

        #endregion
    }
}
