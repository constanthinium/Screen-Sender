using System.Windows;

namespace Screen_Sender
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendMenuItem_Click(object sender, RoutedEventArgs e)
        {
            InputDialog inputDialog = new InputDialog("Enter receiver IP address. Leave empty to loopback");
            if (inputDialog.ShowDialog() == true)
            {
                clientMenuItem.IsEnabled = false;
                frame.Content = new SenderPage(inputDialog.Input);
            }
        }

        private void ReceiveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            clientMenuItem.IsEnabled = false;
            frame.Content = new ReceiverPage();
        }

        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "If you want to show your screen - click Client/Send, if you want to view somebody's screen - click Client/Receive.",
                "Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
