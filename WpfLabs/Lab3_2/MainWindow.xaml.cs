using System;
using System.Collections.Generic;
using System.IO;
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

namespace Lab3_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Info _dc;

        public MainWindow()
        {
            InitializeComponent();
            _dc = new Info();
            DataContext = _dc;
        }

        private void SaveOnClick(object sender, RoutedEventArgs e)
        {
            File.WriteAllLines("employees.txt", _dc.Employees);
        }

        private void AddOnClick(object sender, RoutedEventArgs e)
        {
            string error = string.Empty;
            foreach (FrameworkElement child in Container.Children)
            {
                BindingExpression bindingExpression = child.GetBindingExpression(TextBox.TextProperty) ?? child.GetBindingExpression(ComboBox.TextProperty);
                if (bindingExpression != null)
                {
                    bindingExpression.UpdateSource();
                    if (Validation.GetHasError(child))
                    {
                        error = Validation.GetErrors(child)[0].Exception.Message;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(error))
            {
                _dc.Employees.Add(_dc);
            }
            else
            {
                MessageBox.Show(this, error, "Проверка данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
