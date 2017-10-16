using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;

namespace AirlineTicketOffice.Main.Services.Dialog
{
    /// <summary>
    /// This class: open word file in any files directory
    /// and convert to xps file. Return via NavigateViewModel
    /// in TariffsView.
    /// </summary>
    public class WordFileDialogService : IWordFileDialogService
    {
        #region fields

        public string FilePath { get; set; }
        public IDocumentPaginatorSource Document { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Open word file in any files directory
        /// and convert to xps file. Return via NavigateViewModel</summary>
        /// in TariffsView.
        /// <returns>
        /// true(if ok) or false...
        /// </returns>
        public bool OpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Multiselect = false;

            string path = AppDomain.CurrentDomain.BaseDirectory;

            dlg.InitialDirectory = Path.GetFullPath(Path.Combine(path, "..\\Files"));

            dlg.Filter = "document(*.doc, *docx)|*.doc;*.docx";

            dlg.DefaultExt = ".doc";

            if (dlg.ShowDialog() == true && dlg.FileName.Length > 0)
            {
                FilePath = (dlg.FileName);

                string newXPSDocumentName = String.Concat(Path.GetDirectoryName(dlg.FileName), "\\",
                           Path.GetFileNameWithoutExtension(dlg.FileName), ".xps");

                // Set DocumentViewer.Document to XPS document
                this.Document =
                    ConvertWordDocToXPSDoc(dlg.FileName, newXPSDocumentName, dlg.FileName).GetFixedDocumentSequence();

                return true;
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

        /// <summary>
        /// This method takes a Word document full path and new XPS document full path and name
        /// and returns the new XpsDocument
        /// </summary>
        /// <param name="wordDocName"></param>
        /// <param name="xpsDocName"></param>
        /// <returns></returns>
        private XpsDocument ConvertWordDocToXPSDoc(string wordDocName, string xpsDocName, string dlgPath)
        {
            // Create a WordApplication and add Document to it
            Microsoft.Office.Interop.Word.Application wordApplication = 
                                            new Microsoft.Office.Interop.Word.Application();

            wordApplication.Documents.Add(wordDocName);


            Document doc = wordApplication.ActiveDocument;
           
            try
            {
                object fileName = xpsDocName;
                object SaveFormat = WdSaveFormat.wdFormatXPS;
                object oMissing = System.Reflection.Missing.Value;
                object AllowSubstitutions = true;
                object isVisible = true;

                // check file existing:
                DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(dlgPath));

                FileInfo[] xpsFiles = dir.GetFiles("*.xps", SearchOption.TopDirectoryOnly);

                foreach (var f in xpsFiles)
                {
                    if (f.FullName == xpsDocName)
                    {
                        break;
                    }
                    else
                    {
                        doc.SaveAs(ref fileName, ref SaveFormat, ref oMissing,
                  ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                  ref oMissing, ref oMissing, ref oMissing, ref isVisible, ref oMissing,
                  ref AllowSubstitutions, ref oMissing, ref oMissing);
                    }
                }

                wordApplication.Quit();

                XpsDocument xpsDoc = new XpsDocument(xpsDocName, FileAccess.Read);
                return xpsDoc;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return null;
        }

        #endregion
    }
}
