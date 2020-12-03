using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Screen_Sender
{
    public partial class ReceiverPage : Page
    {
        public ReceiverPage()
        {
            InitializeComponent();

            UdpClient server = new UdpClient(new IPEndPoint(IPAddress.Any, (int)Application.Current.Properties["port"]));
            IPEndPoint client = new IPEndPoint(IPAddress.Any, 0);
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += delegate
            {
                while (true)
                {
                    byte[] data = server.Receive(ref client);
                    Dispatcher.Invoke(() =>
                    {
                        image.Source = BytesToImageSource(data);
                    });
                }
            };
            backgroundWorker.RunWorkerAsync();

        }

        ImageSource BytesToImageSource(byte[] data)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(data);
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}
