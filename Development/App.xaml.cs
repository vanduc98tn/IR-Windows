using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Development
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void Application_Startup(object sender, StartupEventArgs e)
        {
            var exists = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1;
            if (exists)
            {
                MessageBox.Show("The application has already run!", "Note", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown();
                return;
            }

            // Await the startup method if it's async
            SystemsManager.Instance.StartUp();
            UiManager.Instance.Startup();
           
        }
    }
}
