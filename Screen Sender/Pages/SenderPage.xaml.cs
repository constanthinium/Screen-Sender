using System.Windows.Controls.Primitives;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;

namespace Screen_Sender
{
    public partial class SenderPage : Page
    {
        readonly DispatcherTimer dispatcherTimer;

        public SenderPage(string address)
        {
            InitializeComponent();

            ImageConverter converter = new ImageConverter();

            IPAddress ipAddress = address.Length != 0 ? IPAddress.Parse(address) : IPAddress.Loopback;
            UdpClient client = new UdpClient();
            int port = (int)Application.Current.Properties["port"];
            client.Connect(ipAddress, port);
            Log($"connected to {ipAddress}:{port}");

            var screenSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            Bitmap bitmap = new Bitmap(screenSize.Width, screenSize.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            dispatcherTimer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (s, e) =>
            {
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                byte[] data = (byte[])converter.ConvertTo(ScaleBitmap(bitmap, (int)scaleSlider.Value), typeof(byte[]));
                if (data.Length <= ushort.MaxValue)
                {
                    client.Send(data, data.Length);
                    Log($"sent {data.Length} bytes");
                }
                else
                {
                    Log("too many bytes, couldn't send");
                }
            }, Dispatcher);
            dispatcherTimer.Start();
        }

        static Bitmap ScaleBitmap(Bitmap bitmap, int backscale) =>
            new Bitmap(bitmap, bitmap.Width / backscale, bitmap.Height / backscale);

        void Log(string message)
        {
            if (logTextBox != null)
            {
                logTextBox.AppendText(DateTime.Now.ToLongTimeString() + ": " + message + '\n');
                logTextBox.ScrollToEnd();
            }
        }

        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Log("backscale: " + e.NewValue);
        }

        private void IntervalSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (dispatcherTimer != null)
            {
                dispatcherTimer.Interval = TimeSpan.FromSeconds(e.NewValue);
                Log("interval: " + e.NewValue);
            }
        }
    }
}
