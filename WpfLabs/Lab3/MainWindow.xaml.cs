using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Values values;
        public MainWindow()
        {
            InitializeComponent();
            values = new Values()
            {
                XStart = 1,
                XStop = 3,
                Step = .1,
                N = 3
            };
            this.DataContext = values;
        }

        private void CalculateOnClick(object sender, RoutedEventArgs e)
        {
            values.Results.Clear();
            double first, second;
            for (double x = values.XStart; x <= values.XStop; x += values.Step)
            {
                first = S(x);
                second = Y(x);
                values.Results.Add(string.Format("S({0}) = {1}, Y({0}) = {2}", x, first, second));
            }
        }

        double S(double x)
        {
            double result = 0;
            for (int k = 1; k <= values.N; k++)
            {
                result += Math.Pow(-1, k) * Math.Cos(k * x) / k / k;
            }

            return result;
        }

        double Y(double x)
        {
            return 0.25 * (x * x - Math.PI * Math.PI / 3);
        }
    }
}
