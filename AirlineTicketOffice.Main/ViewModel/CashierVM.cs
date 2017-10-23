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

namespace AirlineTicketOffice.Main.ViewModel
{
    public sealed class CashierVM : ViewModelBase
    {
        #region constructor
        public CashierVM(ICashierRepository repository,
                         IMainNavigationService navigationService)
        {
            _repository = repository;
            _navigationService = navigationService;

            this.Cashier = new CashierModel();

            this.ButtonLoadVisible = "Hidden";

            Task.Factory.StartNew(() =>
            {
                lock (locker)
                {
                    this.Cashiers = new ObservableCollection<CashierModel>(_repository.GetAll());
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

            ReceiveCashier();
        }
        #endregion

        #region fields

        private readonly ICashierRepository _repository;

        private readonly IMainNavigationService _navigationService;

        private ObservableCollection<CashierModel> _Cashiers;

        private CashierModel _Cashier;

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

        public CashierModel Cashier
        {
            get { return _Cashier; }
            set { Set(() => Cashier, ref _Cashier, value); }

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


        public ObservableCollection<CashierModel> Cashiers
        {
            get { return _Cashiers; }
            set { Set(() => Cashiers, ref _Cashiers, value); }
        }

        #endregion

        #region commands      

        private ICommand _getAllCashierCommand;


        public ICommand GetAllCashierCommand
        {
            get
            {
                if (_getAllCashierCommand == null)
                {
                    _getAllCashierCommand = new RelayCommand(() =>
                    {
                        Task.Factory.StartNew(() =>
                        {
                            this.Cashiers = new ObservableCollection<CashierModel>(_repository.GetAll());
                        });

                    });
                }
                return _getAllCashierCommand;
            }
            set { _getAllCashierCommand = value; }
        }


        /// <summary>
        /// The method to send the selected Cashier from the DataGrid on UI
        /// to the View Model
        /// </summary>
        /// <param name="p"></param>
        private ICommand _sendCashierCommand;

        public ICommand SendCashierCommand
        {
            get
            {
                if (_sendCashierCommand == null)
                {
                    _sendCashierCommand = new RelayCommand<CashierModel>((c) =>
                    {
                        if (c != null)
                        {
                            Messenger.Default.Send<MessageSendCashier>(new MessageSendCashier()
                            {
                                SendCashier = c
                            });
                        }
                    });
                }
                return _sendCashierCommand;
            }
            set { _sendCashierCommand = value; }
        }


        /// <summary>
        /// Update Cashier in db via repository.
        /// </summary>
        private ICommand _saveCashierChangeCommand;

        public ICommand SaveCashierChangeCommand
        {
            get
            {
                if (_saveCashierChangeCommand == null)
                {
                    _saveCashierChangeCommand = new RelayCommand<CashierModel>((p) =>
                    {

                        try
                        {
                            if (_repository.Update(p))
                            {
                                RaisePropertyChanged("Cashier");
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
                            Debug.WriteLine("'SaveCashierChangeCommand' method fail..." + ex.Message);
                        }

                    });
                }
                return _saveCashierChangeCommand;
            }
            set { _saveCashierChangeCommand = value; }
        }       

        private ICommand _sendCashierToTicketCommand;

        /// <summary>
        /// Send this.Cashier to NewTicket view model.
        /// </summary>
        public ICommand SendCashierToTicketCommand
        {
            get
            {
                if (_sendCashierToTicketCommand == null)
                {
                    _sendCashierToTicketCommand = new RelayCommand(() =>
                    {
                        if (this.Cashier != null)
                        {
                            _navigationService.NavigateTo("NewTicketViewKey", "New Ticket Window");

                            Messenger.Default.Send<MessageCashierToNewTicket>(new MessageCashierToNewTicket()
                            {
                                SendCashierFromCashierVM = this.Cashier
                            });

                            Messenger.Default.Send<MessageStatus>(new MessageStatus()
                            {
                                MessageStatusFromFlight = "New Ticket Window"
                            });

                        }

                    });
                }
                return _sendCashierToTicketCommand;
            }
            set { _sendCashierToTicketCommand = value; }
        }


        #endregion

        #region methods

        /// <summary>
        /// The Method used to Receive the send Cashier from the DataGrid UI
        /// and assigning it the the Cashier Notifiable property so that
        /// it will be displayed on the other view.
        /// </summary>
        void ReceiveCashier()
        {
            if (this.Cashier != null)
            {
                Messenger.Default.Register<MessageSendCashier>(this, (c) => {
                    this.Cashier = c.SendCashier;
                });
            }
        }

        #endregion
    }
}
