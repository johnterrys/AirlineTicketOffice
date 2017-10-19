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
    public sealed class AllTicketsVM : ViewModelBase
    {

        #region constructor
        public AllTicketsVM(ITicketRepository repository)
        {
            _repository = repository;

            Task.Factory.StartNew(() =>
            {
                lock(locker)
                {
                    this.Tickets = new ObservableCollection<AllTicketsModel>(_repository.GetAll());
                }             

                Application.Current.Dispatcher.Invoke(
                      new Action(() =>
                      {
                          this.DataGridVisibility = "Collapsed";

                      }));
            });


        }
        #endregion

        #region fields

        private readonly ITicketRepository _repository;

        private ObservableCollection<AllTicketsModel> _tickets;

        private AllTicketsModel _ticket;

        private string _dataGridVisibility;

        object locker = new object();

        #endregion

        #region properties

        public string DataGridVisibility
        {
            get { return _dataGridVisibility; }
            set { Set(() => DataGridVisibility, ref _dataGridVisibility, value); }
        }

        public AllTicketsModel Ticket
        {
            get { return _ticket; }
            set { Set(() => Ticket, ref _ticket, value); }
        }

        public ObservableCollection<AllTicketsModel> Tickets
        {
            get { return _tickets; }
            set { Set(() => Tickets, ref _tickets, value); }
        }

        #endregion

        #region commands      

        private ICommand _getAllTicketCommand;

        private ICommand _sendTicketCommand;

       
        public ICommand GetAllTicketCommand
        {
            get
            {
                if (_getAllTicketCommand == null)
                {
                    _getAllTicketCommand = new RelayCommand(() =>
                    {
                        Task.Factory.StartNew(() =>
                        {
                            this.Tickets = new ObservableCollection<AllTicketsModel>(_repository.GetAll());
                        });
                        
                    });
                }
                return _getAllTicketCommand;
            }
            set { _getAllTicketCommand = value; }
        }

        public ICommand SendTicketCommand
        {
            get
            {
                if (_sendTicketCommand == null)
                {
                    _sendTicketCommand = new RelayCommand<AllTicketsModel>((t) =>
                    {
                        if (t != null)
                        {
                            Messenger.Default.Send<MessageCommunicator>(new MessageCommunicator()
                            {
                                AllTicketMessage = t
                            });
                        }
                    });
                }
                return _sendTicketCommand;
            }
            set { _sendTicketCommand = value; }
        }


        #endregion



    }
}