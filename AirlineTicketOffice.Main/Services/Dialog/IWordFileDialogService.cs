using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace AirlineTicketOffice.Main.Services.Dialog
{
    internal interface IWordFileDialogService:IFileDialogService<WordFileDialogService>
    {
        IDocumentPaginatorSource Document { get; set; }
    }
}
