using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PracIG
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        const int NumberRows = 4;
        const int NumberCols = 4;

        UniformGrid uniGrid;
        int xEmpty, yEmpty, iCounter;
        Key[] keys = {Key.Left, Key.Right, Key.Up, Key.Down};
        Random rand;
        UIElement elEmptySpare = new Empty();
        public MainWindow()
        {
            InitializeComponent();

            Title = "Jeu de Tacquin";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            Background = SystemColors.ControlBrush;

            StackPanel stack = new StackPanel();
            Content = stack;

            Button btn = new Button();
            btn.Content = "_Scramble";
            btn.Margin = new Thickness(10);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Click += ScrambleOnClick;
            stack.Children.Add(btn);

            Border bord = new Border();
            bord.BorderBrush = SystemColors.ControlDarkBrush;
            bord.BorderThickness = new Thickness(1);
            stack.Children.Add(bord);

            uniGrid = new UniformGrid();
            uniGrid.Rows = NumberRows;
            uniGrid.Columns = NumberCols;
            bord.Child = uniGrid;

            for(int i=0;i<NumberRows * NumberCols -1; i++)
            {
                Tile tile = new Tile();
                tile.Text = (i + 1).ToString();
                tile.MouseLeftButtonDown += TileOnMouseLeftButtonDown;
                uniGrid.Children.Add(tile);
            }

            uniGrid.Children.Add(new Empty());
            xEmpty = NumberCols - 1;
            yEmpty = NumberRows - 1;
        }

        void TileOnMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            Tile tile = sender as Tile;
            int iMove = uniGrid.Children.IndexOf(tile);
            int xMove = iMove % NumberCols;
            int yMove = iMove / NumberCols;

            if(xMove == xEmpty)
            {
                while(yMove != yEmpty)
                {
                    MoveTile(xMove, yEmpty + (yMove - yEmpty) / Math.Abs(yMove - yEmpty));
                }
            }
            if (yMove == yEmpty)
            {
                while (yMove != yEmpty)
                {
                    MoveTile(xEmpty + (xMove - xEmpty) / Math.Abs(xMove - xEmpty), yMove);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.Right:
                    MoveTile(xEmpty - 1, yEmpty); break;
                case Key.Left:
                    MoveTile(xEmpty + 1, yEmpty); break;
                case Key.Down:
                    MoveTile(xEmpty, yEmpty-1); break;
                case Key.Up:
                    MoveTile(xEmpty, yEmpty + 1); break;
            }
        }

        void ScrambleOnClick(object sender, RoutedEventArgs e)
        {
            rand = new Random();
            iCounter = 16 * NumberCols * NumberRows;

            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromMilliseconds(10);
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }

        void TimerOnTick(object sender, EventArgs args)
        {
            for(int i = 0; i < 5; i++)
            {
                MoveTile(xEmpty, yEmpty + rand.Next(3) - 1);
                MoveTile(xEmpty + rand.Next(3) - 1, yEmpty);
            }
            if(0 == iCounter--)
            {
                (sender as DispatcherTimer).Stop();
            }
        }

        void MoveTile(int xTile, int yTile)
        {
            if ((xTile == xEmpty && yTile == yEmpty) ||
                xTile < 0 || xTile >= NumberCols ||
                yTile < 0 || yTile >= NumberRows)
                return;

            int iTile = NumberCols * yTile + xTile;
            int iEmpty = NumberCols * yEmpty + xEmpty;

            UIElement elTile = uniGrid.Children[iTile];
            UIElement elEmpty = uniGrid.Children[iEmpty];

            uniGrid.Children.RemoveAt(iTile);
            uniGrid.Children.Insert(iEmpty, elEmptySpare);
            uniGrid.Children.RemoveAt(iEmpty);
            uniGrid.Children.Insert(iEmpty, elTile);

            xEmpty = xTile;
            yEmpty = yTile;
            elEmptySpare = elEmpty;
        }
    }
}
