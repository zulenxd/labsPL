// Задания 2 и 3, Вариант 4
// Класс RightTriangle (прямоугольный треугольник).
// Поля: double a, double b — длины катетов.
//
// Задание 2: метод вычисления площади.
// Задание 3: перегрузка операций (++, --, явное double, неявное bool, <=, >=).

using System;

namespace Task2And3
{
    // =========================================================
    // КЛАСС ПРЯМОУГОЛЬНОГО ТРЕУГОЛЬНИКА
    // =========================================================

    class RightTriangle
    {
        // Приватные поля — доступны только внутри класса
        private double _a;   // первый катет
        private double _b;   // второй катет

        // --------------------------------------------------
        // СВОЙСТВА (Properties) — «умные» геттер и сеттер
        // --------------------------------------------------

        /// <summary>
        /// Свойство для катета A с проверкой корректности.
        /// get — возвращает значение поля.
        /// set — устанавливает значение с проверкой (value — то, что присваивают).
        /// </summary>
        public double A
        {
            get { return _a; }
            set
            {
                if (value <= 0)
                {
                    // ArgumentException — исключение для неверных аргументов
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

        // --------------------------------------------------
        // КОНСТРУКТОРЫ
        // --------------------------------------------------

        /// <summary>
        /// Конструктор по умолчанию — катеты равны 1.
        /// </summary>
        public RightTriangle()
        {
            _a = 1.0;
            _b = 1.0;
        }

        /// <summary>
        /// Конструктор с параметрами.
        /// Используем свойства A и B — они автоматически проверят значения.
        /// </summary>
        public RightTriangle(double a, double b)
        {
            A = a;   // вызывает сеттер свойства A (с проверкой)
            B = b;   // вызывает сеттер свойства B (с проверкой)
        }

        // --------------------------------------------------
        // ЗАДАНИЕ 2: МЕТОД ВЫЧИСЛЕНИЯ ПЛОЩАДИ
        // --------------------------------------------------

        /// <summary>
        /// Вычисляет площадь прямоугольного треугольника.
        /// Формула: S = (a * b) / 2
        /// </summary>
        public double CalculateArea()
        {
            return 0.5 * _a * _b;
        }

        /// <summary>
        /// Красивый вывод объекта.
        /// :F2 — форматирование double до 2 знаков после запятой.
        /// </summary>
        public override string ToString()
        {
            return $"Прямоугольный треугольник: a={_a:F2}, b={_b:F2}, S={CalculateArea():F2}";
        }

        // --------------------------------------------------
        // ЗАДАНИЕ 3: ПЕРЕГРУЗКА ОПЕРАЦИЙ
        // --------------------------------------------------

        // === Унарная операция ++ ===
        // Увеличивает оба катета в 2 раза (чтобы снова получить катеты прямоугольного треугольника).
        // static — метод принадлежит классу, а не конкретному объекту.
        // operator ++ — говорим компилятору, что перегружаем оператор ++.
        // Возвращаем НОВЫЙ объект (не изменяем текущий).
        public static RightTriangle operator ++(RightTriangle t)
        {
            return new RightTriangle(t._a * 2, t._b * 2);
        }

        // === Унарная операция -- ===
        // Уменьшает оба катета в 2 раза.
        public static RightTriangle operator --(RightTriangle t)
        {
            return new RightTriangle(t._a / 2, t._b / 2);
        }

        // === Явное (explicit) приведение к double ===
        // Использование: double d = (double)triangle;
        // Если треугольник существует (катеты > 0) — возвращает площадь.
        // Иначе — возвращает отрицательное число (-1).
        //
        // explicit = нужно явно писать (double) при приведении типа.
        public static explicit operator double(RightTriangle t)
        {
            if (t._a > 0 && t._b > 0)
            {
                return t.CalculateArea();
            }

            return -1.0;   // треугольник не существует
        }

        // === Неявное (implicit) приведение к bool ===
        // Использование: bool b = triangle; — без скобок (неявное).
        // Возвращает true, если треугольник существует (оба катета > 0).
        //
        // implicit = C# автоматически преобразует тип, когда это нужно.
        public static implicit operator bool(RightTriangle t)
        {
            return t._a > 0 && t._b > 0;
        }

        // === Операция <= (сравнение площадей) ===
        // Возвращает true, если площадь t1 меньше или равна площади t2.
        // В C# операции <= и >= ВСЕГДА перегружаются ПАРАМИ.
        public static bool operator <=(RightTriangle t1, RightTriangle t2)
        {
            return t1.CalculateArea() <= t2.CalculateArea();
        }

        // === Операция >= (сравнение площадей) ===
        // Обязательно нужна вместе с <=.
        public static bool operator >=(RightTriangle t1, RightTriangle t2)
        {
            return t1.CalculateArea() >= t2.CalculateArea();
        }
    }

    // =========================================================
    // ПРОГРАММА — ТЕСТИРОВАНИЕ
    // =========================================================

    class Program
    {
        /// <summary>
        /// Считывает положительное вещественное число с консоли.
        /// double.TryParse — пытается преобразовать строку в double.
        /// </summary>
        static double ReadPositiveDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // Заменяем запятую на точку — на случай разных локалей
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
            Console.WriteLine("============================================");
            Console.WriteLine("   ЗАДАНИЯ 2 и 3 — Вариант 4");
            Console.WriteLine("============================================\n");

            // --------------------------------------------------
            // ЗАДАНИЕ 2: тестирование конструкторов и метода CalculateArea
            // --------------------------------------------------
            Console.WriteLine("--- Задание 2: вычисление площади ---\n");

            // Конструктор по умолчанию
            var tDefault = new RightTriangle();
            Console.WriteLine($"Конструктор по умолчанию: {tDefault}");

            Console.WriteLine();

            // Конструктор с параметрами + обработка исключений
            Console.WriteLine("Введите данные для первого треугольника:");
            double a1 = ReadPositiveDouble("  Катет a: ");
            double b1 = ReadPositiveDouble("  Катет b: ");

            // try-catch — перехватываем исключение, если что-то пошло не так
            RightTriangle t1;
            try
            {
                t1 = new RightTriangle(a1, b1);
            }
            catch (ArgumentException ex)
            {
                // ex.Message — текст сообщения об ошибке
                Console.WriteLine($"Ошибка при создании объекта: {ex.Message}");
                return;   // выходим из Main
            }

            Console.WriteLine($"\nТреугольник t1: {t1}");
            Console.WriteLine($"Площадь t1: {t1.CalculateArea():F2}");

            // --------------------------------------------------
            // ЗАДАНИЕ 3: тестирование перегруженных операций
            // --------------------------------------------------
            Console.WriteLine("\n--- Задание 3: перегрузка операций ---\n");

            // Тест оператора ++
            Console.WriteLine($"t1 до оператора ++: {t1}");
            t1++;   // вызывает наш перегруженный ++
            Console.WriteLine($"t1 после оператора ++ (катеты ×2): {t1}");

            Console.WriteLine();

            // Тест оператора --
            Console.WriteLine($"t1 до оператора --: {t1}");
            t1--;   // вызывает наш перегруженный --
            Console.WriteLine($"t1 после оператора -- (катеты ÷2): {t1}");

            Console.WriteLine();

            // Тест явного приведения к double
            // (double) — явное приведение, нужно написать вручную
            double areaViaConversion = (double)t1;
            Console.WriteLine($"Явное приведение к double (площадь): (double)t1 = {areaViaConversion:F2}");

            Console.WriteLine();

            // Тест неявного приведения к bool
            // bool b = t1 — C# сам вызывает наш оператор implicit bool
            bool triangleExists = t1;
            Console.WriteLine($"Неявное приведение к bool (существует ли треугольник): {triangleExists}");

            Console.WriteLine();

            // Тест операций <= и >=
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

            Console.WriteLine();
            Console.WriteLine($"t1: {t1}");
            Console.WriteLine($"t2: {t2}");
            Console.WriteLine();
            Console.WriteLine($"Площадь t1 = {t1.CalculateArea():F2}");
            Console.WriteLine($"Площадь t2 = {t2.CalculateArea():F2}");
            Console.WriteLine();

            // Используем перегруженные операторы
            Console.WriteLine($"t1 <= t2 (площадь t1 ≤ площадь t2): {t1 <= t2}");
            Console.WriteLine($"t1 >= t2 (площадь t1 ≥ площадь t2): {t1 >= t2}");
        }
    }
}