using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System;
using System.Windows.Documents;

namespace PracWpf
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Title = "Set FontSize Property";

            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            FontSize = 16;
            double[] fntSizes = { 8, 16, 32 };

            Grid grid = new Grid();
            Content = grid;

            for(int i = 0; i < 2; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < fntSizes.Length; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < fntSizes.Length; i++)
            {
                Button btn = new Button();
                btn.Content = new TextBlock(
                    new Run("Set window FontSize To " + fntSizes[i]));
                btn.Tag = fntSizes[i];
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += WindowFontSizeOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 0);
                Grid.SetColumn(btn, i);

                btn = new Button();
                btn.Content = new TextBlock(
                    new Run("Set button FontSizeTo " + fntSizes[i]));
                btn.Tag = fntSizes[i];
                (btn.Content as TextBlock).FontSize = 12;
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += ButtonFontSizeOnClick;
                grid.Children.Add(btn);
                Grid.SetRow(btn, 1);
                Grid.SetColumn(btn, i);
            }
        }
        void WindowFontSizeOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;
            FontSize = (double)btn.Tag;
        }

        void ButtonFontSizeOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;
            btn.FontSize = (double)btn.Tag;
        }
    }
}
