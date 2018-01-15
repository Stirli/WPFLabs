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
        // два поля, представляющих точность
        private int toleranceNum;
        private double tolerance;

        // Конструктор класса окна
        public MainWindow()
        {
            InitializeComponent();
            // Получаем разделитель из региональных настроек системы
            DecimalSeparatorButton.Content = CultureInfo.InstalledUICulture.NumberFormat.NumberDecimalSeparator;
            // Задаем точность
            toleranceNum = 7;
            tolerance = Math.Pow(10, -toleranceNum);
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
            return d.ToString("0." + new string('#', toleranceNum), CultureInfo.InstalledUICulture);
        }

        // Преобразует строку в double с учетом региональных параметров.
        private double StringToDouble(string text)
        {
            return double.Parse(text, CultureInfo.InstalledUICulture);
        }

        void Calculate()
        {
            try
            {
                CurrentValueBox.Text = Process(CurrentValueBox.Text);
            }
            catch (Exception e)
            {
                CurrentValueBox.Text = e.Message;
            }
        }

        private string Process(string text)
        {
            Regex mainReg = new Regex(@"^(?<op1>-?\d+,\d+|-?\d+)" + @"(?<func>[\+\-\*\/])" + @"(?<op2>-?\d+,\d+|-?\d+)$");
            Match match = mainReg.Match(text);
            if (match.Success)
            {
                double op1 = StringToDouble(match.Groups["op1"].Value);
                double op2 = StringToDouble(match.Groups["op2"].Value);
                double result = Calc(match.Groups["func"].Value, op1, op2);
                return DoubleToString(result);
            }
            throw new FormatException();
        }

        double Calc(string @operator, double op1, double op2 = 0)
        {
            double result;
            switch (@operator)
            {
                case "-":
                    result = op1 - op2;
                    break;
                case "+":
                    result = op1 + op2;
                    break;
                case "*":
                    result = op1 * op2;
                    break;
                case "/":
                    if (Math.Abs(op2) < tolerance)
                    {
                        throw new DivideByZeroException();
                    }

                    result = op1 / op2;
                    break;
                case "sin":
                    result = Math.Sin(WithCurrentMode(op1));
                    break;
                case "cos":
                    result = Math.Cos(WithCurrentMode(op1));
                    break;
                case "tg":
                    op1 = WithCurrentMode(op1);
                    if (Math.Abs(op1) > tolerance && Math.Abs(op1 % (1.5 * Math.PI)) < tolerance || Math.Abs(op1 - (Math.PI / 2)) < tolerance)
                    {
                        throw new InvalidOperationException("Тангенс не существует.");
                    }

                    result = Math.Tan(op1);
                    break;
                case "^2":
                    result = op1 * op1;
                    break;
                case " 1\n—\n x":
                    if (Math.Abs(op1) < tolerance)
                    {
                        throw new DivideByZeroException();
                    }

                    result = 1 / op1;
                    break;
                case "√":
                    result = Math.Sqrt(op1);
                    if (double.IsNaN(result))
                        throw new InvalidOperationException("Невозможно вычислить корень.");
                    break;
                default: throw new InvalidOperationException("Неверный оператор или функция");
            }
            return result;
        }

        // Числа
        private void CipherButtonOnClick(object sender, RoutedEventArgs e)
        {
            if ((CurrentValueBox.Text.Equals("0")))
                CurrentValueBox.Text = string.Empty;
            Button button = sender as Button;
            CurrentValueBox.Text += button.Content.ToString();
        }

        // Нажатие на +/-
        private void NegateButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentValueBox.Text.Length == 0)
            {
                return;
            }

            Match lastNumString = LastNumString();
            if (lastNumString.Success)
                if (lastNumString.Value.First() != '-')
                {
                    CurrentValueBox.Text = CurrentValueBox.Text.Substring(0, lastNumString.Index) + "-" + lastNumString.Value;
                }
                else
                {
                    CurrentValueBox.Text = CurrentValueBox.Text.Substring(0, lastNumString.Index) + lastNumString.Value.Substring(1, lastNumString.Length - 1);
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
        }

        // Очистка СЕ
        private void ResetButtonOnClick(object sender, RoutedEventArgs e)
        {
            CurrentValueBox.Text = "0";
        }

        // Удалить символ
        private void BackspaceButtonOnClick(object sender, RoutedEventArgs e)
        {
            CurrentValueBox.Text = CurrentValueBox.Text.Length > 1 ? CurrentValueBox.Text.Substring(0, CurrentValueBox.Text.Length - 1) : "0";
        }

        // Переключение DEG RAD GRAD
        private void DegRadGradButtonOnClick(object sender, RoutedEventArgs e)
        {
            ModeBlock.Content = (sender as Button).Content;
        }


        private void PiButtonOnClick(object sender, RoutedEventArgs e)
        {
            CurrentValueBox.Text += DoubleToString(Math.PI);
        }


        // Нажатие на + - * / %  sin cos tg
        private void BinOpButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            CurrentValueBox.Text += button.Content;
        }

        // унарные операции, изменяющие текущее число
        private void UnoOpButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {

                Match lastNumString = LastNumString();
                Button button = sender as Button;
                string func = button.Content.ToString();
                string calcResult = DoubleToString(Calc(func, StringToDouble(lastNumString.Value)));
                CurrentValueBox.Text = CurrentValueBox.Text.Substring(0, lastNumString.Index) + calcResult;
            }
            catch (Exception ex)
            {
                CurrentValueBox.Text = ex.Message;
            }
        }

        private Match LastNumString()
        {
            Regex r = new Regex(@"(-?\d+,\d+|-?\d+)", RegexOptions.RightToLeft);
            Match match = r.Match(CurrentValueBox.Text);
            return match;
        }

        private void ResultButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Calculate();
        }
    }
}
