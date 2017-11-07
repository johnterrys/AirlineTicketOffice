using AirlineTicketOffice.Model.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Xml.Linq;

namespace AirlineTicketOffice.Main.Services.Dialog
{
    public class XmlDialogService : IXmlDialogService
    {
        #region ctor
        public XmlDialogService()
        {
            /* Set CultureInfo("ru-RU"). */
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

            current = CultureInfo.CurrentCulture;
        }
        #endregion

        #region fields

        private CultureInfo current;

        public string FilePath { get; set; }

        public PassengerModel PassengerFromXml { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Open xml file in any files directory
        /// and ret
        /// </summary>
        /// in TariffsView.
        /// <returns>
        /// true(if all ok) or false...
        /// </returns>
        public bool OpenFileDialog()
        {
          
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Multiselect = false;

            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Docs"));

            dlg.InitialDirectory = path;

            dlg.Filter = "document(*.xml)|*.xml";

            dlg.DefaultExt = ".xml";

            try
            {
                if (dlg.ShowDialog() == true && dlg.FileName.Length > 0)
                {
                    FilePath = (dlg.FileName);

                    XElement xel = XElement.Load(FilePath);
                    IEnumerable<XElement> passengers = xel.Elements();

                    foreach (var p in passengers)
                    {
                        this.PassengerFromXml = new PassengerModel()
                        {
                            Citizenship = p.Element("Citizenship").Value,
                            PassportNumber = p.Element("PassportNumber").Value,
                            Sex = p.Element("Sex").Value,
                            FullName = p.Element("FullName").Value,
                            DateOfBirth = DateTime.Parse(p.Element("DateOfBirth").Value),
                            TermOfPassportDate = DateTime.Parse(p.Element("TermOfPassportDate").Value),
                            CountryOfResidence = p.Element("CountryOfResidence").Value,
                            PhoneMobile = p.Element("PhoneMobile").Value,
                            Email = p.Element("Email").Value
                        };

                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);

                return false;
            }

        }

        /// <summary>
        /// Save xml file in any files directory
        /// with Passenger data.
        /// </summary>
        public bool SavePassenger(PassengerModel passenger)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Docs"));

            dlg.InitialDirectory = path;

            dlg.Filter = "document(*.xml)|*.xml";

            dlg.DefaultExt = ".xml";

            try
            {
                if (dlg.ShowDialog() == true && passenger != null)
                {
                    XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("Passengers"));


                    xDoc.Element("Passengers").Add(new XElement("Passenger",
                        new XElement("Citizenship", passenger.Citizenship),
                        new XElement("PassportNumber", passenger.PassportNumber),
                        new XElement("Sex", passenger.Sex),
                        new XElement("FullName", passenger.FullName),
                        new XElement("DateOfBirth", passenger.DateOfBirth),
                        new XElement("TermOfPassportDate", passenger.TermOfPassportDate),
                        new XElement("CountryOfResidence", passenger.CountryOfResidence),
                        new XElement("PhoneMobile", passenger.PhoneMobile),
                        new XElement("Email", passenger.Email)));


                    xDoc.Save(dlg.FileName);

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }          
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
