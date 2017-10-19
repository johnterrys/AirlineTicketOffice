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
    public sealed class NewTicketVM:ViewModelBase
    {
        #region constructor
        public NewTicketVM(ITicketRepository repository)
        {
            _ticketRepository = repository;

            this.NewTicket = new AllTicketsModel();


            this.ForegroundForUser = "#f2f2f2";
            this.MessageForUser = "Please, Enter A Data...";


        }
        #endregion

        #region fields

        private readonly ITicketRepository _ticketRepository;

        private AllTicketsModel _newTicket;

        private string _ForegroundForUser;

        private string _MessageForUser;


        #endregion

        #region properties

        public AllTicketsModel NewTicket
        {
            get { return _newTicket; }
            set { Set(() => NewTicket, ref _newTicket, value); }

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
        /// Create new ticket in db via repository.
        /// </summary>
        private ICommand _saveNewTicketCommand;

        public ICommand SaveNewTicketCommand
        {
            get
            {
                if (_saveNewTicketCommand == null)
                {
                    _saveNewTicketCommand = new RelayCommand<AllTicketsModel>((t) =>
                    {

                        try
                        {
                            if (_ticketRepository.Add(t))
                            {
                                RaisePropertyChanged("NewTicket");
                                this.NewTicket = new AllTicketsModel();
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
                            Debug.WriteLine("'SaveNewTicketCommand' fail..." + ex.Message);
                        }

                    });
                }
                return _saveNewTicketCommand;
            }
            set { _saveNewTicketCommand = value; }
        }

        #endregion

        #region methods

       

        #endregion
    }
}
