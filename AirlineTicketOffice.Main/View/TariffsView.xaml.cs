using AirlineTicketOffice.Main.Services.Navigation;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirlineTicketOffice.Main.View
{
    /// <summary>
    /// Interaction logic for TariffsView.xaml
    /// </summary>
    public partial class TariffsView : UserControl
    {
        public TariffsView()
        {
            InitializeComponent();

            NavigationSetup();
        }

        /// <summary>
        /// TEMPORARY DIRTY HACK!!!!!!!!
        /// </summary>
        private void NavigationSetup()
        {
            Messenger.Default.Register<NavigateArgs>(this, (x) =>
            {
                DocumentViewer dw = null;

                if (x.Token == "word")
                {
                    GridForWebBrowser.Children.Clear();

                    dw = new DocumentViewer();

                    GridForWebBrowser.Children.Add(dw);

                    dw.Document = x.Document;
                }
                if (x.Token == "pdf")
                {
                    if (File.Exists(x.Url))
                    {
                        try
                        {
                            Process.Start(x.Url);
                        }                       
                        catch (Win32Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                        catch (ObjectDisposedException ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                        catch (FileNotFoundException ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }

                    }

                }               

            });
        }
    }
}