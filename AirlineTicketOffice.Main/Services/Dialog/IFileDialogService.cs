using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace AirlineTicketOffice.Main.Services.Dialog
{
    public interface IFileDialogService<T> where T: class
    {
        string FilePath { get; set; }    
        bool OpenFileDialog();
        void ShowMessage(string message);
    }
}