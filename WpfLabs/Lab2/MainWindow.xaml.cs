using System;
using System.Collections.Generic;
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

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void NoButton_OnMouse(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            FrameworkElement parent = (FrameworkElement)button.Parent;
            Point mousePositionOnButton = e.GetPosition(button);
            double bx = mousePositionOnButton.X;
            double by = mousePositionOnButton.Y;

            double delta = 5;

            double left = bx < button.Width / 2
                ? button.Margin.Left + bx + delta
                : button.Margin.Left - (button.Width - bx) - delta;

            if (left < 0)
            {
                left = parent.ActualWidth - button.ActualWidth;
            }
            else
            if (left + button.ActualWidth >= parent.ActualWidth)
            {
                left = 0;
            }

            double top = by < button.ActualHeight / 2
                ? button.Margin.Top + by + delta
                : button.Margin.Top - (button.ActualHeight - by) - delta;

            if (top < 0)
            {
                top = parent.ActualHeight - button.ActualHeight;
            }
            else
            if (top + button.ActualHeight >= parent.ActualHeight)
            {
                top = 0;
            }

            button.Margin = new Thickness(left, top, button.Margin.Right, button.Margin.Bottom);
        }

        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException("СООБЩЕНИЕ");
        }
    }
}
