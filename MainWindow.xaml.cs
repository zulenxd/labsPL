using System;
using System.Windows;
using Task2And3; // Подключаем пространство имен твоего класса RightTriangle

namespace lab9
{
    public partial class MainWindow : Window
    {
        private RightTriangle _t1;
        private RightTriangle _t2;

        public MainWindow()
        {
            InitializeComponent();
            Log("Приложение запущено. Введите данные для треугольников.");
        }

        // Вспомогательный метод для вывода текста в лог
        private void Log(string message)
        {
            LogBlock.Text += message + "\n";
            LogBlock.Text += new string('-', 40) + "\n";
        }

        // Вспомогательный метод для безопасного чтения чисел из TextBox
        private double ParseDouble(string input, string fieldName)
        {
            input = input.Replace(',', '.');
            if (double.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double result) && result > 0)
            {
                return result;
            }
            throw new FormatException($"Поле '{fieldName}' должно быть положительным числом!");
        }

        private void BtnCreateT1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = ParseDouble(TextBoxT1_A.Text, "Катет a (t1)");
                double b = ParseDouble(TextBoxT1_B.Text, "Катет b (t1)");

                _t1 = new RightTriangle(a, b);
                Log($"Создан t1: {_t1}");
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка логики", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCreateT2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = ParseDouble(TextBoxT2_A.Text, "Катет a (t2)");
                double b = ParseDouble(TextBoxT2_B.Text, "Катет b (t2)");

                _t2 = new RightTriangle(a, b);
                Log($"Создан t2: {_t2}");
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка логики", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnT1PlusPlus_Click(object sender, RoutedEventArgs e)
        {
            if (_t1 == null)
            {
                MessageBox.Show("Сначала создайте треугольник t1!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            _t1++;
            Log($"Применен оператор ++ к t1. Результат:\n{_t1}");
        }

        private void BtnT1MinusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (_t1 == null)
            {
                MessageBox.Show("Сначала создайте треугольник t1!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            _t1--;
            Log($"Применен оператор -- к t1. Результат:\n{_t1}");
        }

        private void BtnT1Double_Click(object sender, RoutedEventArgs e)
        {
            if (_t1 == null)
            {
                MessageBox.Show("Сначала создайте треугольник t1!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            double area = (double)_t1;
            Log($"Явное приведение t1 к double (Площадь): {area:F2}");
        }

        private void BtnT1Bool_Click(object sender, RoutedEventArgs e)
        {
            if (_t1 == null)
            {
                MessageBox.Show("Сначала создайте треугольник t1!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            bool exists = _t1;
            Log($"Неявное приведение t1 к bool (Существование): {exists}");
        }

        private void BtnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (_t1 == null || _t2 == null)
            {
                MessageBox.Show("Для сравнения необходимо создать оба треугольника (t1 и t2)!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            bool lessOrEqual = _t1 <= _t2;
            bool greaterOrEqual = _t1 >= _t2;

            Log($"Сравнение площадей:\nПлощадь t1 = {_t1.CalculateArea():F2}\nПлощадь t2 = {_t2.CalculateArea():F2}\n" +
                $"t1 <= t2 : {lessOrEqual}\n" +
                $"t1 >= t2 : {greaterOrEqual}");
        }

        private void BtnClearLog_Click(object sender, RoutedEventArgs e)
        {
            LogBlock.Text = "";
        }
    }
}