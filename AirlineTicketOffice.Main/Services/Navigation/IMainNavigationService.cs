using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineTicketOffice.Main.Services.Navigation
{
    public interface IMainNavigationService : INavigationService
    {
        object Parameter { get; }

    }
}