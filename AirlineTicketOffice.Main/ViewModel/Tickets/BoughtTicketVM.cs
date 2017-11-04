using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using AirlineTicketOffice.Model.IRepository;
using System.Collections.ObjectModel;
using AirlineTicketOffice.Model.Models;
using System.Threading.Tasks;
using System.Windows;


/// <summary>
/// View model for 'BoughtTicketView.xaml'.
/// </summary>
namespace AirlineTicketOffice.Main.ViewModel.Tickets
{
    public sealed class BoughtTicketVM:ViewModelBase
    {
        #region constructor
        public BoughtTicketVM(IBoughtTicketRepository repository)
        {

            this.ButtonVisible = false;
            this.DataGridVisibility = true;

            _repository = repository;          

            Task.Factory.StartNew(() =>
            {
                lock(locker)
                {
                    this.Tickets = new ObservableCollection<BoughtTicketModel>(_repository.GetAll());
                }
                
                Application.Current.Dispatcher.Invoke(
                      new Action(() =>
                      {
                          this.DataGridVisibility = false;
                          this.ButtonVisible = true;
                      }));
            });


        }
        #endregion

        #region fields

        private readonly IBoughtTicketRepository _repository;

        private ObservableCollection<BoughtTicketModel> _tickets;

        private bool _dataGridVisibility;

        private bool _ButtonVisible;

        object locker = new object(); 

        #endregion

        #region properties

        public bool ButtonVisible
        {
            get { return _ButtonVisible; }
            set { Set(() => ButtonVisible, ref _ButtonVisible, value); }
        }

        public bool DataGridVisibility
        {
            get { return _dataGridVisibility; }
            set { Set(() => DataGridVisibility, ref _dataGridVisibility, value); }
        }      

        public ObservableCollection<BoughtTicketModel> Tickets
        {
            get { return _tickets; }
            set { Set(() => Tickets, ref _tickets, value); }
        }

        #endregion

        #region commands      

        private ICommand _getBoughtTicketCommand;

        
        public ICommand GetBoughtTicketCommand
        {
            get
            {
                if (_getBoughtTicketCommand == null)
                {
                    _getBoughtTicketCommand = new RelayCommand(() =>
                    {
                        Task.Factory.StartNew(() =>
                        {
                            this.Tickets = new ObservableCollection<BoughtTicketModel>(_repository.GetAll());
                        });

                    });
                }
                return _getBoughtTicketCommand;
            }
            set { _getBoughtTicketCommand = value; }
        }        

        #endregion

    }
}
