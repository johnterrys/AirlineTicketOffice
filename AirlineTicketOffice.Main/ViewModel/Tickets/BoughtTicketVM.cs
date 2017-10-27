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

namespace AirlineTicketOffice.Main.ViewModel.Tickets
{
    public sealed class BoughtTicketVM:ViewModelBase
    {
        #region constructor
        public BoughtTicketVM(IBoughtTicketRepository repository)
        {
            _repository = repository;

            this.ButtonLoadVisible = "Hidden";

            Task.Factory.StartNew(() =>
            {
                lock(locker)
                {
                    this.Tickets = new ObservableCollection<BoughtTicketModel>(_repository.GetAll());
                }
                
                Application.Current.Dispatcher.Invoke(
                      new Action(() =>
                      {
                          this.DataGridVisibility = "Collapsed";
                          this.ButtonLoadVisible = "Visible";
                      }));
            });


        }
        #endregion

        #region fields

        private readonly IBoughtTicketRepository _repository;

        private ObservableCollection<BoughtTicketModel> _tickets;

        //private BoughtTicketModel _ticket;

        private string _dataGridVisibility;

        private string _ButtonLoadVisible;

        object locker = new object(); 

        #endregion

        #region properties

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

        //public BoughtTicketModel Ticket
        //{
        //    get { return _ticket; }
        //    set { Set(() => Ticket, ref _ticket, value); }
        //}

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

        //private ICommand _sendTicketCommand;

        //public ICommand SendTicketCommand
        //{
        //    get
        //    {
        //        if (_sendTicketCommand == null)
        //        {
        //            _sendTicketCommand = new RelayCommand<AllTicketsModel>((t) =>
        //            {
        //                if (t != null)
        //                {
        //                    Messenger.Default.Send<MessageAllTicket>(new MessageAllTicket()
        //                    {
        //                        AllTicketMessage = t
        //                    });
        //                }
        //            });
        //        }
        //        return _sendTicketCommand;
        //    }
        //    set { _sendTicketCommand = value; }
        //}

        #endregion

    }
}
