//лаба 6 задание 2 3 вариант 4

using System;

namespace Task2And3
{
    class RightTriangle
    {
        private double _a;
        private double _b;

        public double A
        {
            get { return _a; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Длина катета должна быть положительной.");
                }
                _a = value;
            }
        }

        public double B
        {
            get { return _b; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Длина катета должна быть положительной.");
                }
                _b = value;
            }
        }

        public RightTriangle()
        {
            _a = 1.0;
            _b = 1.0;
        }

        public RightTriangle(double a, double b)
        {
            A = a;
            B = b;
        }

        public double CalculateArea()
        {
            return 0.5 * _a * _b;
        }

        public override string ToString()
        {
            return $"Прямоугольный треугольник: a={_a:F2}, b={_b:F2}, S={CalculateArea():F2}";
        }

        public static RightTriangle operator ++(RightTriangle t)
        {
            return new RightTriangle(t._a * 2, t._b * 2);
        }

        public static RightTriangle operator --(RightTriangle t)
        {
            return new RightTriangle(t._a / 2, t._b / 2);
        }

        public static explicit operator double(RightTriangle t)
        {
            if (t._a > 0 && t._b > 0)
            {
                return t.CalculateArea();
            }
            return -1.0;
        }

        public static implicit operator bool(RightTriangle t)
        {
            return t._a > 0 && t._b > 0;
        }

        public static bool operator <=(RightTriangle t1, RightTriangle t2)
        {
            return t1.CalculateArea() <= t2.CalculateArea();
        }

        public static bool operator >=(RightTriangle t1, RightTriangle t2)
        {
            return t1.CalculateArea() >= t2.CalculateArea();
        }
    }

    class Program
    {
        static double ReadPositiveDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                input = input.Replace(',', '.');

                if (double.TryParse(input,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double result) && result > 0)
                {
                    return result;
                }

                Console.WriteLine("  Ошибка: введите положительное число (например: 3.5)!");
            }
        }

        static void Main(string[] args)
        {
            var tDefault = new RightTriangle();
            Console.WriteLine($"Конструктор по умолчанию: {tDefault}\n");

            Console.WriteLine("Введите данные для первого треугольника:");
            double a1 = ReadPositiveDouble("  Катет a: ");
            double b1 = ReadPositiveDouble("  Катет b: ");

            RightTriangle t1;
            try
            {
                t1 = new RightTriangle(a1, b1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка при создании объекта: {ex.Message}");
                return;
            }

            Console.WriteLine($"\nТреугольник t1: {t1}");
            Console.WriteLine($"Площадь t1: {t1.CalculateArea():F2}");

            Console.WriteLine($"t1 до оператора ++: {t1}");
            t1++;
            Console.WriteLine($"t1 после оператора ++ (катеты ×2): {t1}\n");

            Console.WriteLine($"t1 до оператора --: {t1}");
            t1--;
            Console.WriteLine($"t1 после оператора -- (катеты ÷2): {t1}\n");

            double areaViaConversion = (double)t1;
            Console.WriteLine($"Явное приведение к double (площадь): (double)t1 = {areaViaConversion:F2}\n");

            bool triangleExists = t1;
            Console.WriteLine($"Неявное приведение к bool (существует ли треугольник): {triangleExists}\n");

            Console.WriteLine("Введите данные для второго треугольника:");
            double a2 = ReadPositiveDouble("  Катет a: ");
            double b2 = ReadPositiveDouble("  Катет b: ");

            RightTriangle t2;
            try
            {
                t2 = new RightTriangle(a2, b2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка при создании объекта: {ex.Message}");
                return;
            }

            Console.WriteLine($"\nt1: {t1}");
            Console.WriteLine($"t2: {t2}\n");
            Console.WriteLine($"Площадь t1 = {t1.CalculateArea():F2}");
            Console.WriteLine($"Площадь t2 = {t2.CalculateArea():F2}\n");

            Console.WriteLine($"t1 <= t2 (площадь t1 ≤ площадь t2): {t1 <= t2}");
            Console.WriteLine($"t1 >= t2 (площадь t1 ≥ площадь t2): {t1 >= t2}");
        }
    }
}