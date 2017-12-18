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

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeButtons();
        }


        private void InitializeButtons()
        {

            for (int i = 1; i < 11; i++)
            {
                Button button = new Button
                {
                    Content = i % 10,
                    Background = new SolidColorBrush(Colors.White)
                };
                button.Click += CipherButton_OnClick;
                ButtonContainer.Children.Add(button);
            }

            {
                Button button = new Button
                {
                    Content = "+/-",
                    Background = new SolidColorBrush(Colors.LightGray)
                };
                button.Click += NegateButton_OnClick;
                ButtonContainer.Children.Insert(9, button);
            }

            {
                Button button = new Button
                {
                    Content = ",",
                    Background = new SolidColorBrush(Colors.LightGray)
                };
                button.Click += DotButton_OnClick;
                ButtonContainer.Children.Add(button);
            }

            string[] captions = {"+", "sin", "-", "cos", "*", "tg", "/"};
            for (var i = 0; i < captions.Length; i++)
            {
                string c = new[] {"+", "sin", "-", "cos", "*", "tg", "/"}[i];
                Button button = new Button
                {
                    Content = c,
                    Background = new SolidColorBrush(Colors.LightGray)
                };
                button.Click += BinOpButton_OnClick;
                AriphContainer.Children.Add(button);
            }
        }


        // Нажатие на + - * / %  sin cos tg
        private void BinOpButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        // Числа
        private void CipherButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null) ResultBox.Text += button.Content.ToString();
        }

        // Нажатие на +/-
        private void NegateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ResultBox.Text.Length > 0)
                if (ResultBox.Text[0] != '-')
                {
                    ResultBox.Text = '-' + ResultBox.Text;
                }
                else
                {
                    ResultBox.Text = ResultBox.Text.Substring(1, ResultBox.Text.Length - 1);
                }
        }

        // Нажатие на десятичный разделитель
        private void DotButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ResultBox.Text.Contains(','))
                return;

            ResultBox.Text += ',';
        }
    }
}
