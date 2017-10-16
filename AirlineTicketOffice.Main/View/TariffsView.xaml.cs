using AirlineTicketOffice.Main.Services.Navigation;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
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

        private void NavigationSetup()
        {
            Messenger.Default.Register<NavigateArgs>(this, (x) =>
            {
                DocumentViewer dw = null;

                WebBrowser wb = null;

                if (x.Token == "word")
                {
                    GridForWebBrowser.Children.Clear();

                    dw = new DocumentViewer();

                    GridForWebBrowser.Children.Add(dw);

                    dw.Document = x.Document;
                }
                if (x.Token == "pdf")
                {                
                    GridForWebBrowser.Children.Clear();

                    wb = new WebBrowser();

                    GridForWebBrowser.Children.Add(wb);

                    wb.Navigate(new Uri(x.Url, UriKind.Absolute));
                }               

            });
        }
    }
}