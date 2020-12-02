using System.Windows.Threading;
using System.Windows;

namespace Screen_Sender
{
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Exception: " + e.Exception.Message, "Exception");
            e.Handled = true;
        }
    }
}
