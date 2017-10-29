using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace AirlineTicketOffice.Main.Services.Dialog
{
    /// <summary>
    /// Show message box with assembly info.
    /// </summary>
    public class DialogAbout : IDialogMessage
    {

        private string assemblyTitle;

        private string version;

        private string assemblyDescription;

        private string copyright;

        private string message;

        public bool Show()
        {
            try
            {
                Assembly MainAssembly = Assembly.Load(Assembly.GetExecutingAssembly().FullName);

                foreach (Attribute a in MainAssembly.GetCustomAttributes(true))
                {
                    if (a is AssemblyTitleAttribute)
                        assemblyTitle = (a as AssemblyTitleAttribute).Title;

                    if (a is AssemblyFileVersionAttribute)
                        version = (a as AssemblyFileVersionAttribute).Version;

                    if (a is AssemblyDescriptionAttribute)
                        assemblyDescription = (a as AssemblyDescriptionAttribute).Description;

                    if (a is AssemblyCopyrightAttribute)
                        copyright = (a as AssemblyCopyrightAttribute).Copyright;

                }

                message = String.Format("Program: {0}\nVersion: {1}\nGitHub: {2}\n{3}",
                                         assemblyTitle,
                                         version,
                                         assemblyDescription,
                                         copyright);

                MessageBox.Show(message, "About program:");

                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("'DialogAbout' Show() method fail..." + ex.Message);
                return false;
            }
          
        }
    }
}
