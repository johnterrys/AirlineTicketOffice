using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace AirlineTicketOffice.Main.Services.Dialog
{
    /// <summary>
    /// Start new process.
    /// </summary>
    public class NewProcessGo : INewProcessGo
    {
        public bool startNewProcess(string path)
        {
           
            try
            {
                if (File.Exists(path))
                {

                    Process.Start(path);

                }

                return false;
            }
            catch (Win32Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            catch (FileNotFoundException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
