using AirlineTicketOffice.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Main.Services.Dialog
{
    public interface IXmlDialogService:IFileDialogService<XmlDialogService>
    {
        PassengerModel PassengerFromXml { get; }

        bool SavePassenger(PassengerModel passenger);
    }
}
