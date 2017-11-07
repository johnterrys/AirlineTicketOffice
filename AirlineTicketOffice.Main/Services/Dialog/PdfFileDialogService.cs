using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace AirlineTicketOffice.Main.Services.Dialog
{
    public class PdfFileDialogService : IPdfFileDialogService
    {
        #region ctor

        public PdfFileDialogService(INewProcessGo newProcess)
        {
            _newProcess = newProcess;
        }

        #endregion


        #region prop and fields

        private readonly INewProcessGo _newProcess;
        public string FilePath { get; set; }

        #endregion

        public bool OpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Multiselect = false;

            string path = AppDomain.CurrentDomain.BaseDirectory;

            dlg.InitialDirectory = Path.GetFullPath(Path.Combine(path, "Docs"));

            dlg.Filter = "document(*.pdf)|*.pdf";

            dlg.DefaultExt = ".pdf";

            if (dlg.ShowDialog() == true && dlg.FileName.Length > 0)
            {
                /// <summary>
                /// start New Process: open pdf document if exist.
                /// </summary>  
                if (_newProcess.startNewProcess(dlg.FileName))
                {
                    return true;
                }

            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

    }
}
