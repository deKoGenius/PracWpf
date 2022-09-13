using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush brush = new SolidColorBrush();
        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 192;
            Height = 192;
            brush.Color.ScA = Colors.White;
            Background = brush;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if(e.Key == Key.Up)
            {
                Width *= 1.1;
                Height *= 1.1;
                Left = (SystemParameters.PrimaryScreenWidth - Width)/2;
                Top = (SystemParameters.PrimaryScreenHeight - Height)/2;
            }
            else if (e.Key == Key.Down)
            {
                Width /= 1.1;
                Height /= 1.1;
                Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
                Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
            }
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            if (e.Text == "\b" && Title.Length > 0)
            {
                Title = Title.Substring(0, Title.Length - 1);
            }
            else if (e.Text.Length > 0 && !Char.IsControl(e.Text[0]))
            {
                Title += e.Text;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult resulte = MessageBox.Show("Do you want to save your data?",
                Application.Current.MainWindow.Title,
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);

            e.Cancel = (resulte == MessageBoxResult.Yes || resulte == MessageBoxResult.Cancel);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            double width = ActualWidth - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight
                - SystemParameters.CaptionHeight;

            Point ptMouse = e.GetPosition(this);
            Point ptCenter = new Point(width / 2, height / 2);
            Vector vectMouse = ptMouse - ptCenter;
            double angle = Math.Atan2(vectMouse.Y, vectMouse.X);
            Vector vectEllipse = new Vector(width / 2 * Math.Cos(angle), height / 2 * Math.Sin(angle));
            Byte byLevel = (byte)(255 * (1 - Math.Min(1, vectMouse.Length / vectEllipse.Length)));
            Color clr = brush.Color;
            clr.R = clr.G = clr.B = byLevel;
            brush.Color = clr;
            Background = brush;
            System.Console.WriteLine($"R:{brush.Color.R}, G:{brush.Color.G}, B:{brush.Color.B}");
        }
    }
}
