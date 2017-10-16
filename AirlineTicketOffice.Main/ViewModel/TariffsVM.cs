using AirlineTicketOffice.Main.Services.Dialog;
using AirlineTicketOffice.Model.IRepository;
using AirlineTicketOffice.Model.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
using WPFNavigation.ViewModel;

namespace AirlineTicketOffice.Main.ViewModel
{
    public class TariffsVM:NavigateViewModel
    {
        #region constructor
        public TariffsVM(ITariffsRepository tariffRepository,
                         IPdfFileDialogService pdfFileDialogService,
                         IWordFileDialogService wordFileDialogService)
        {

            _tariffsRepository = tariffRepository;
            _pdfFileDialogService = pdfFileDialogService;
            _wordFileDialogService = wordFileDialogService;
            this.ButtonWordFileVisible = "Hidden";
            this.ButtonLoadVisible = "Hidden";
            this.ButtonPdfFileVisible = "Hidden";

            Task.Factory.StartNew(() =>
            {
                this.Tariffs = new ObservableCollection<TariffModel>(_tariffsRepository.GetAll());

                Application.Current.Dispatcher.Invoke(
                      new Action(() =>
                      {
                          this.DataGridVisibility = "Collapsed";
                          this.ButtonWordFileVisible = "Visible";
                          this.ButtonPdfFileVisible = "Visible";
                          this.ButtonLoadVisible = "Visible";

                      }));

            });


        }
        #endregion

        #region fields

        private readonly ITariffsRepository _tariffsRepository;

        private readonly IPdfFileDialogService _pdfFileDialogService;

        private readonly IWordFileDialogService _wordFileDialogService;

        private ObservableCollection<TariffModel> _tariffs;     

        private string _dataGridVisibility;

        private string _ButtonLoadVisible;

        private string _ButtonWordFileVisible;

        private string _ButtonPdfFileVisible;


        # endregion

        #region properties

        public string DataGridVisibility
        {
            get { return _dataGridVisibility; }
            set { Set(() => DataGridVisibility, ref _dataGridVisibility, value); }
        }

        public string ButtonPdfFileVisible
        {
            get { return _ButtonPdfFileVisible; }
            set { Set(() => ButtonPdfFileVisible, ref _ButtonPdfFileVisible, value); }
        }

        public string ButtonWordFileVisible
        {
            get { return _ButtonWordFileVisible; }
            set { Set(() => ButtonWordFileVisible, ref _ButtonWordFileVisible, value); }
        }

        public string ButtonLoadVisible
        {
            get { return _ButtonLoadVisible; }
            set { Set(() => ButtonLoadVisible, ref _ButtonLoadVisible, value); }
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
                                string absUri = _pdfFileDialogService.FilePath;

                                Navigate(absUri, "pdf");
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
        /// Command open pdf and another file via set
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
                                string filePath = _wordFileDialogService.FilePath;

                                Navigate(_wordFileDialogService.Document, "word");
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


        #endregion


        #region methods


        #endregion

    }
}
