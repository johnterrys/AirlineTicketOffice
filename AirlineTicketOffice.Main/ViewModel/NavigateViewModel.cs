using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using AirlineTicketOffice.Main.Services.Navigation;
using System.Windows.Documents;

namespace WPFNavigation.ViewModel
{
    public class NavigateViewModel : ViewModelBase
    {
        public NavigateViewModel()
        {
        }

        public void Navigate(string url, string token)
        {
            Messenger.Default.Send<NavigateArgs>(new NavigateArgs(url, token));
        }

        public void Navigate(IDocumentPaginatorSource document, string token)
        {
            Messenger.Default.Send<NavigateArgs>(new NavigateArgs(document, token));
        }
    }
}