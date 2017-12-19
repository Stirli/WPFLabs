using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        // Указывает, нужно ли вводить число заново.
        private bool reset = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Нажатие на + - * / %  sin cos tg
        private void BinOpButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ExpressionBox.Text = CurrentValueBox.Text + " " + button.Content;
            reset = true;
        }

        private void UnoOpButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var oper = (sender as Button).Content.ToString();
                double result;
                if (oper.Equals("π"))
                {
                    result = Math.PI;
                }
                else
                {
                    double x = double.Parse(CurrentValueBox.Text);
                    switch (oper)
                    {
                        case "sin":
                            result = Math.Sin(WithCurrentMode(x));
                            break;
                        case "cos":
                            result = Math.Cos(WithCurrentMode(x));
                            break;
                        case "tg":
                            result = Math.Tan(WithCurrentMode(x));
                            break;
                        case "^2":
                            result = x * x;
                            break;
                        case " 1\n—\n x":
                            result = 1 / x;
                            break;
                        case "√":
                            result = Math.Sqrt(x);
                            if (double.IsNaN(result))
                                throw new Exception("Невозможно вычислить корень из отрицательного числа.");
                            break;
                        default: throw new Exception((sender as Button).Content.ToString());
                    }
                }
                CurrentValueBox.Text = result.ToString();
                reset = true;
            }
            catch (FormatException ex)
            {
                CurrentValueBox.Text = string.IsNullOrEmpty(CurrentValueBox.Text) ? "Ошибка - пустая строка" : CurrentValueBox.Text + ":" + ex.Message;
            }
            catch (Exception ex)
            {
                CurrentValueBox.Text = ex.Message;
            }
        }

        // Переводит в радианы в зависимости от режима
        private double WithCurrentMode(double value)
        {
            switch (ModeBlock.Text)
            {
                case "DEG":
                    return Math.PI * value / 180.0;
                case "RAD":
                    return value;
                case "GRAD":
                    return Math.PI * value / 200;
            }
            return double.NaN;
        }


        private void ResultButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (ExpressionBox.Text.Length > 0)
            {
                char lastChar = ExpressionBox.Text.Last();
                double op1 = double.Parse(ExpressionBox.Text.Split(' ').First());
                double op2 = double.Parse(CurrentValueBox.Text);
                double result = double.NaN;
                ExpressionBox.Text += " " + CurrentValueBox.Text + " = ";
                switch (lastChar)
                {
                    case '-':
                        result = op1 - op2;
                        break;
                    case '+':
                        result = op1 + op2;
                        break;
                    case '*':
                        result = op1 * op2;
                        break;
                    case '/':
                        result = op1 / op2;
                        break;
                }
                CurrentValueBox.Text = result.ToString();
                ExpressionBox.Text += result;
            }

            reset = true;
        }

        // Числа
        private void CipherButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (reset)
            {
                CurrentValueBox.Text = button.Content.ToString();
                reset = false;
            }
            else
            {
                CurrentValueBox.Text += button.Content;
            }
        }

        // Нажатие на +/-
        private void NegateButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentValueBox.Text.Length > 0)
                if (CurrentValueBox.Text.First() != '-')
                {
                    CurrentValueBox.Text = '-' + CurrentValueBox.Text;
                }
                else
                {
                    CurrentValueBox.Text = CurrentValueBox.Text.Substring(1, CurrentValueBox.Text.Length - 1);
                }
        }

        // Нажатие на десятичный разделитель
        private void DotButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentValueBox.Text.Contains(','))
                return;

            CurrentValueBox.Text += ',';
        }

        private void CleanCurrentButtonOnClick(object sender, RoutedEventArgs e)
        {
            CurrentValueBox.Text = "0";
        }

        private void ResetButtonOnClick(object sender, RoutedEventArgs e)
        {
            CurrentValueBox.Text = "0";
            ExpressionBox.Text = string.Empty;
        }

        private void BackspaceButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (!reset)
            {
                CurrentValueBox.Text = CurrentValueBox.Text.Length > 0 ? CurrentValueBox.Text.Substring(0, CurrentValueBox.Text.Length - 1) : "0";
            }
        }

        private void DegRadGradButtonOnClick(object sender, RoutedEventArgs e)
        {
            ModeBlock.Text = (sender as Button).Content.ToString();
        }
    }
}
