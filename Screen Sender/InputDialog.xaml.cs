using System.Windows.Input;
using System.Windows;

namespace Screen_Sender
{
    public partial class InputDialog : Window
    {
        public string Input => textBox.Text;

        public InputDialog(string message, string title = "")
        {
            InitializeComponent();

            okButton.Click += (s, e) => OK();
            cancelButton.Click += (s, e) => Cancel();

            textBlock.Text = message;
            Title = title;
        }

        void OK()
        {
            DialogResult = true;
            Close();
        }

        void Cancel()
        {
            DialogResult = false;
            Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    OK();
                    break;
                case Key.Escape:
                    Cancel();
                    break;
            }
        }
    }
}
