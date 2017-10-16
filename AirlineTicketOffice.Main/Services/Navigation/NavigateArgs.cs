using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace AirlineTicketOffice.Main.Services.Navigation
{
    public class NavigateArgs
    {
        public NavigateArgs()
        {
        }

        public NavigateArgs(string url, string token)
        {
            Url = url;
            Token = token;
        }

        public NavigateArgs(IDocumentPaginatorSource document, string token)
        {
            Document = document;
            Token = token;
        }

        public string Url { get; set; }

        public IDocumentPaginatorSource Document { get; set; }

        public string Token { get; set; }
    }
}
