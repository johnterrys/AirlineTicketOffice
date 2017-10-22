using AirlineTicketOffice.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Main.Services.Messenger
{
    public class MessageCommunicator
    {
        public AllTicketsModel AllTicketMessage { get; set; }

        public BoughtTicketModel BoughtTicketMessage { get; set; }

        public PassengerModel SendPassenger { get; set; }

        public FlightModel SendFlight { get; set; }

        public string MessageStatusFromFlight { get; set; }
    }
}
