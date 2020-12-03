using System.Windows.Threading;
using System.Windows;

namespace Screen_Sender
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Current.Properties.Add("port", 80);
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Exception: " + e.Exception.Message, "Exception");
            e.Handled = true;
        }
    }
}
