using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;


namespace AirlineTicketOffice.Main.Services.Dialog
{
    /// <summary>
    /// This class: open word file in any files directory
    /// and convert to xps file. Return via NavigateViewModel
    /// in TariffsView.
    /// </summary>
    public class WordFileDialogService : IWordFileDialogService
    {
        #region ctor

        public WordFileDialogService(INewProcessGo newProcess)
        {
            _newProcess = newProcess;
        }

        #endregion

        #region fields

        public string FilePath { get; set; }

        private readonly INewProcessGo _newProcess;

        #endregion

        #region methods

        /// <summary>
        /// Open word file in new process.
        /// true(if ok) or false...
        /// </returns>
        public bool OpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Multiselect = false;

            string path = AppDomain.CurrentDomain.BaseDirectory;

            dlg.InitialDirectory = Path.GetFullPath(Path.Combine(path, "Docs"));

            dlg.Filter = "document(*.doc, *docx)|*.doc;*.docx";

            dlg.DefaultExt = ".doc";

            if (dlg.ShowDialog() == true && dlg.FileName.Length > 0)
            {
                /// <summary>
                /// start New Process: open word document if exist.
                /// </summary>  
                if (_newProcess.startNewProcess(dlg.FileName))
                {
                    return true;
                }
               
            }
            return false;
        }

        
        /// <summary>
        /// Show error message.
        /// </summary>    
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }    

        #endregion
    }
}
