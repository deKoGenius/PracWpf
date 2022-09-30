using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace PracWpf
{
    internal class UriDialog : Window
    {
        TextBox txtBox;
        public UriDialog()
        {
            Title = "Enter a URI";
            ShowInTaskbar = false;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            txtBox = new TextBox();
            txtBox.Margin = new Thickness(48);
            Content = txtBox;

            txtBox.Focus();
        }

        public string Text
        {
            get { return txtBox.Text; }
            set
            {
                txtBox.Text = value;
                txtBox.SelectionStart = txtBox.Text.Length;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Close();
            }
        }
    }
}
