using AirlineTicketOffice.Main.Services.Dialog;
using AirlineTicketOffice.Main.Services.Messenger;
using AirlineTicketOffice.Main.Services.Navigation;
using AirlineTicketOffice.Model.IRepository;
using AirlineTicketOffice.Model.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AirlineTicketOffice.Main.ViewModel
{
    public class TariffsVM:ViewModelBase
    {
        #region constructor
        public TariffsVM(ITariffsRepository tariffRepository,
                         IPdfFileDialogService pdfFileDialogService,
                         IWordFileDialogService wordFileDialogService,
                         IMainNavigationService navigationService)
        {

            this.DataGridVisibility = true;

            _tariffsRepository = tariffRepository;
            _pdfFileDialogService = pdfFileDialogService;
            _wordFileDialogService = wordFileDialogService;
            _navigationService = navigationService;

            this.ButtonVisible = false;
           
            this.Tariff = new TariffModel();


            Task.Factory.StartNew(() =>
            {
                lock(locker)
                {
                    this.Tariffs = new ObservableCollection<TariffModel>(_tariffsRepository.GetAll());
                }
                
                Application.Current.Dispatcher.Invoke(
                      new Action(() =>
                      {
                          this.DataGridVisibility = false;
                          this.ButtonVisible = true;                         

                      }));

            });

            ReceiveTariff();

        }
        #endregion

        #region fields

        private readonly IMainNavigationService _navigationService;

        object locker = new object();

        private readonly ITariffsRepository _tariffsRepository;

        private readonly IPdfFileDialogService _pdfFileDialogService;

        private readonly IWordFileDialogService _wordFileDialogService;

        private ObservableCollection<TariffModel> _tariffs;

        private TariffModel _tariff;

        private bool _dataGridVisibility;

        private bool _ButtonVisible;


        #endregion

        #region properties

        public TariffModel Tariff
        {
            get { return _tariff; }
            set { Set(() => Tariff, ref _tariff, value); }
        }

        public bool DataGridVisibility
        {
            get { return _dataGridVisibility; }
            set { Set(() => DataGridVisibility, ref _dataGridVisibility, value); }
        }

        public bool ButtonVisible
        {
            get { return _ButtonVisible; }
            set { Set(() => ButtonVisible, ref _ButtonVisible, value); }
        }   

        public ObservableCollection<TariffModel> Tariffs
        {
            get { return _tariffs; }
            set { Set(() => Tariffs, ref _tariffs, value); }
        }

        #endregion

        #region commands      

        private ICommand _getAllTariffsCommand;

        /// <summary>
        /// Get all flight in db.
        /// </summary>
        public ICommand GetAllTariffsCommand
        {
            get
            {
                if (_getAllTariffsCommand == null)
                {
                    _getAllTariffsCommand = new RelayCommand(() =>
                    {
                        try
                        {                          
                            this.Tariffs?.Clear();

                            Task.Factory.StartNew(() =>
                            {
                                this.Tariffs = new ObservableCollection<TariffModel>(_tariffsRepository.GetAll());
                            });


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    });
                }
                return _getAllTariffsCommand;
            }
            set { _getAllTariffsCommand = value; }
        }

        private ICommand _OpenFilePdfCommand;

        /// <summary>
        /// Command open pdf and another file via set
        /// uri in webBrowser.Novigate in TariffsView.
        /// </summary>
        public ICommand OpenFilePdfCommand
        {
            get
            {
                if (_OpenFilePdfCommand == null)
                {
                    _OpenFilePdfCommand = new RelayCommand(() =>
                    {
                      
                        try
                        {
                            if (_pdfFileDialogService.OpenFileDialog() == true)
                            {
                                _pdfFileDialogService.ShowMessage("Could Not Open Pdf File...");
                            }
                        }
                        catch (Exception ex)
                        {
                            _pdfFileDialogService.ShowMessage(ex.Message);
                        }

                     
                    });
                }
                return _OpenFilePdfCommand;
            }
            set { _OpenFilePdfCommand = value; }
        }


        private ICommand _openFileWordCommand;
        /// <summary>
        /// Command open word file via  DocumentViewer.Document and set
        /// uri in webBrowser.Novigate in TariffsView.
        /// </summary>
        public ICommand OpenFileWordCommand
        {
            get
            {
                if (_openFileWordCommand == null)
                {
                    _openFileWordCommand = new RelayCommand(() =>
                    {

                        try
                        {
                            if (_wordFileDialogService.OpenFileDialog() == true)
                            {
                                _wordFileDialogService.ShowMessage("Could Not Open Word File...");
                            }
                        }
                        catch (Exception ex)
                        {
                            _wordFileDialogService.ShowMessage(ex.Message);
                        }


                    });
                }
                return _openFileWordCommand;
            }
            set { _openFileWordCommand = value; }
        }

        private ICommand _sendTariffToTicketCommand;

        /// <summary>
        /// Send this.Cashier to NewTicket view model.
        /// </summary>
        public ICommand SendTariffToTicketCommand
        {
            get
            {
                if (_sendTariffToTicketCommand == null)
                {
                    _sendTariffToTicketCommand = new RelayCommand(() =>
                    {
                        if (this.Tariff != null)
                        {                           

                            Messenger.Default.Send<MessageTariffToNewTicket>(new MessageTariffToNewTicket()
                            {
                                SendTariffFromTariffVM = this.Tariff
                            });

                            Messenger.Default.Send<MessageStatus>(new MessageStatus()
                            {
                                MessageStatusFromFlight = "New Ticket Window"
                            });

                            _navigationService.NavigateTo("NewTicketViewKey", "New Ticket Window");
                        }

                    });
                }
                return _sendTariffToTicketCommand;
            }
            set { _sendTariffToTicketCommand = value; }
        }

        /// <summary>
        /// The method to send the selected Tariff from the DataGrid on UI
        /// to the View Model
        /// </summary>
        /// <param name="p"></param>
        private ICommand _sendTariffCommand;

        public ICommand SendTariffCommand
        {
            get
            {
                if (_sendTariffCommand == null)
                {
                    _sendTariffCommand = new RelayCommand<TariffModel>((t) =>
                    {
                        if (t != null)
                        {
                            Messenger.Default.Send<MessageSendTariff>(new MessageSendTariff()
                            {
                                SendTariff = t
                            });
                        }
                    });
                }
                return _sendTariffCommand;
            }
            set { _sendTariffCommand = value; }
        }

        #endregion


        #region methods

        /// <summary>
        /// The Method used to Receive the send Tariff(Rate) from the DataGrid UI
        /// and assigning it the the Tariff Notifiable property so that
        /// it will be displayed on the other view.
        /// </summary>
        void ReceiveTariff()
        {
            if (this.Tariff != null)
            {
                Messenger.Default.Register<MessageSendTariff>(this, (t) => {
                    this.Tariff = t.SendTariff;
                });
            }
        }

        #endregion

    }
}
