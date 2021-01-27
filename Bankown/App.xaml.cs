
using Bankown.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Bankown
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void ApplicationStart(Object sender, StartupEventArgs args)
        {
            HttpController.Instance.Token = null;

            AuthenticationWindow authWindow = new AuthenticationWindow();
            authWindow.Closing += AuthWindow_Closed;

            authWindow.Show();
        }

        private void AuthWindow_Closed(object sender, EventArgs e)
        {
            // check if authenticated
            AuthenticationWindow authWindow = sender as AuthenticationWindow;
            if (authWindow.User != null)
            {
                Trace.WriteLine("Authenticated");
                new MainWindow(authWindow.User).Show();
            }
        }
    }
}
