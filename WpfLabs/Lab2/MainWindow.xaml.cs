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
            FrameworkElement element = (FrameworkElement)sender;
            // координаты курсора относительно "кнопки"
            Point mousePositionOnElement = e.GetPosition(element);
            FrameworkElement parent = (FrameworkElement)element.Parent;
            // координаты относительно родительского контейнера
            Point mousePositionOnParent = e.GetPosition(parent);
            double bx = mousePositionOnElement.X;
            double by = mousePositionOnElement.Y;
            double px = mousePositionOnParent.X;
            double py = mousePositionOnParent.Y;
            double height = element.ActualHeight;
            double width = element.ActualWidth;

            double delta = 20;

            double left = element.Margin.Left;
            double top = element.Margin.Top;

            if (by < height / 4)
            {
                top = py + delta;
            }
            else if (by >= height * 3 / 4)
            {
                top = py - height - delta;
            }
            else if (bx < width / 2)
            {
                left = px + delta;
            }
            else if (bx >= width / 2)
            {
                left = px - width - delta;
            }

            if (top < 0) top = parent.ActualHeight - height;
            if (top + height > parent.ActualHeight) top = 0;
            if (left < 0) left = parent.ActualWidth - width;
            if (left + width > parent.ActualWidth) left = 0;

            element.Margin = new Thickness(left, top, element.Margin.Right, element.Margin.Bottom);
        }

        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageText.Text = "Ну и живи теперь с этим.";
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageText.Text = "¯\\_(ツ)_/¯";
        }
    }
}
