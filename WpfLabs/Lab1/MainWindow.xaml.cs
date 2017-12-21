using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Указывает, нужно ли вводить число заново. (после нажатия '=' или '+-/*...')
        private bool reset = true;

        // Конструктор класса окна
        public MainWindow()
        {
            InitializeComponent();
            // Получаем разделитель из региональных настроек системы
            DecimalSeparatorButton.Content = CultureInfo.InstalledUICulture.NumberFormat.NumberDecimalSeparator;
        }
      

        // Переводит в радианы в зависимости от режима
        private double WithCurrentMode(double value)
        {
            switch (ModeBlock.Content.ToString())
            {
                case "DEG":
                    return Math.PI * value / 180.0;
                case "RAD":
                    return value;
                case "GRAD":
                    return Math.PI * value / 200;
                default:
                    return double.NaN;
            }
        }

        // Преобразует double в строку с учетом региональных параметров.
        private string DoubleToString(double d)
        {
            return d.ToString("0.#####", CultureInfo.InstalledUICulture);
        }

        // Преобразует строку в double с учетом региональных параметров.
        private double StringToDouble(string text)
        {
            return double.Parse(text, CultureInfo.InstalledUICulture);
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
            if (CurrentValueBox.Text.Length == 0)
            {
                return;
            }

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
            if (CurrentValueBox.Text.Contains(CultureInfo.InstalledUICulture.NumberFormat.NumberDecimalSeparator))
                return;

            CurrentValueBox.Text += CultureInfo.InstalledUICulture.NumberFormat.NumberDecimalSeparator;
        }

        // Очистка С
        private void CleanCurrentButtonOnClick(object sender, RoutedEventArgs e)
        {
            CurrentValueBox.Text = "0";
            reset = true;
        }

        // Очистка СЕ
        private void ResetButtonOnClick(object sender, RoutedEventArgs e)
        {
            CurrentValueBox.Text = "0";
            ExpressionBox.Text = string.Empty;
            reset = true;
        }

        // Удалить символ
        private void BackspaceButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (reset)
            {
                return;
            }

            if (CurrentValueBox.Text.Length > 1)
            {
                CurrentValueBox.Text = CurrentValueBox.Text.Substring(0, CurrentValueBox.Text.Length - 1);
            }
            else
            {
                CurrentValueBox.Text = "0";
                reset = true;
            }
        }

        // Переключение DEG RAD GRAD
        private void DegRadGradButtonOnClick(object sender, RoutedEventArgs e)
        {
            ModeBlock.Content = (sender as Button).Content;
        }


        private void PiButtonOnClick(object sender, RoutedEventArgs e)
        {
            CurrentValueBox.Text = DoubleToString(Math.PI);
        }


        // Нажатие на + - * / %  sin cos tg
        private void BinOpButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Calculate();
                Button button = sender as Button;
                ExpressionBox.Text = CurrentValueBox.Text + " " + button.Content;
            }
            catch (Exception ex)
            {
                CurrentValueBox.Text = ex.Message;
            }
            finally
            {
                reset = true;
            }
        }

        // унарные операции, изменяющие текущее число
        private void UnoOpButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var oper = (sender as Button).Content.ToString();
                double x = StringToDouble(CurrentValueBox.Text);
                double result;
                switch (oper)
                {
                    case "sin":
                        result = Math.Sin(WithCurrentMode(x));
                        break;
                    case "cos":
                        result = Math.Cos(WithCurrentMode(x));
                        break;
                    case "tg":
                        x = WithCurrentMode(x);
                        if (x != 0 && x % (1.5 * Math.PI) == 0 || x == (Math.PI / 2))
                        {
                            throw new ArgumentException("Тангенс не существует.");
                        }

                        result = Math.Tan(x);
                        break;
                    case "^2":
                        result = x * x;
                        break;
                    case " 1\n—\n x":
                        if (x == 0)
                        {
                            throw new DivideByZeroException();
                        }

                        result = 1 / x;
                        break;
                    case "√":
                        result = Math.Sqrt(x);
                        if (double.IsNaN(result))
                            throw new Exception("Невозможно вычислить корень из отрицательного числа.");
                        break;
                    default: throw new Exception((sender as Button).Content.ToString());
                }
                CurrentValueBox.Text = DoubleToString(result);
            }
            catch (Exception ex)
            {
                CurrentValueBox.Text = ex.Message;
            }
            finally
            {
                reset = true;
            }
        }

        private void ResultButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                Calculate();
            }
            catch (Exception e)
            {
                CurrentValueBox.Text = e.Message;
            }
            finally
            {
                reset = true;
            }
        }

        private void Calculate()
        {
            double result = double.NaN;
            if (ExpressionBox.Text.Length > 0)
            {
                char lastChar = ExpressionBox.Text.Last();
                double op1 = StringToDouble(ExpressionBox.Text.Split(' ').First());
                double op2 = StringToDouble(CurrentValueBox.Text);
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
                        if (op2 == 0)
                        {
                            throw new DivideByZeroException();
                        }

                        result = op1 / op2;
                        break;
                }
                string expression = ExpressionBox.Text + " " + CurrentValueBox.Text + " = ";
                string resultString = DoubleToString(result);
                ExpressionBox.Text = String.Empty;
                CurrentValueBox.Text = resultString;

                JournalBox.Text = expression + resultString + "\n" + JournalBox.Text;
                CurrentValueBox.Text = resultString;
            }
        }
    }
}
