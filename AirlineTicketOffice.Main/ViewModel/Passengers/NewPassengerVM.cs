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
    public sealed class NewPassengerVM: ViewModelBase
    {
        #region constructor
        public NewPassengerVM(IPassengerRepository repository)
        {
            _repository = repository;

            this.Passenger = new PassengerModel();

          
            this.ForegroundForUser = "#f2f2f2";
            this.MessageForUser = "Please, Enter A Data...";

 
        }
        #endregion

        #region fields

        private readonly IPassengerRepository _repository;

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
                            if (_repository.Add(p))
                            {
                                RaisePropertyChanged("Passenger");
                                this.Passenger = new PassengerModel();
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
