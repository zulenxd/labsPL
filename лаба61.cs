//лаба 6 задание 1 вариант 4

using System;

namespace Task1
{
    class ThreeNumbers
    {
        protected int _first;
        protected int _second;
        protected int _third;

        public ThreeNumbers(int first, int second, int third)
        {
            _first = first;
            _second = second;
            _third = third;
        }

        public ThreeNumbers(ThreeNumbers other)
        {
            _first = other._first;
            _second = other._second;
            _third = other._third;
        }

        public int FindMinimum()
        {
            int min = _first;

            if (_second < min)
            {
                min = _second;
            }

            if (_third < min)
            {
                min = _third;
            }

            return min;
        }

        public override string ToString()
        {
            return $"Первое: {_first}, Второе: {_second}, Третье: {_third}";
        }
    }

    class Box : ThreeNumbers
    {
        private string _material;

        public Box() : base(1, 1, 1)
        {
            _material = "Неизвестно";
        }

        public Box(int width, int height, int depth, string material)
            : base(width, height, depth)
        {
            _material = material;
        }

        public Box(Box other) : base(other)
        {
            _material = other._material;
        }

        public int CalculateVolume()
        {
            return _first * _second * _third;
        }

        public int CalculateSurfaceArea()
        {
            return 2 * (_first * _second + _second * _third + _first * _third);
        }

        public override string ToString()
        {
            return $"Коробка [{_material}]: Ширина={_first} см, Высота={_second} см, Глубина={_third} см";
        }
    }

    class Program
    {
        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    return result;
                }

                Console.WriteLine("  Ошибка: введите целое число!");
            }
        }

        static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                int value = ReadInt(prompt);

                if (value > 0)
                {
                    return value;
                }

                Console.WriteLine("  Ошибка: число должно быть положительным!");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите три целых числа:");
            int a = ReadInt("  Первое число: ");
            int b = ReadInt("  Второе число: ");
            int c = ReadInt("  Третье число: ");

            var nums1 = new ThreeNumbers(a, b, c);
            Console.WriteLine($"\nОбъект nums1: {nums1}");
            Console.WriteLine($"Минимальное значение: {nums1.FindMinimum()}");

            var nums2 = new ThreeNumbers(nums1);
            Console.WriteLine($"\nКопия nums2 (через конструктор копирования): {nums2}");
            Console.WriteLine($"Минимальное значение копии: {nums2.FindMinimum()}");

            var boxDefault = new Box();
            Console.WriteLine($"Конструктор по умолчанию: {boxDefault}");
            Console.WriteLine($"  Объём: {boxDefault.CalculateVolume()} куб.см");
            Console.WriteLine($"  Площадь поверхности: {boxDefault.CalculateSurfaceArea()} кв.см");
            Console.WriteLine($"  Минимальная сторона: {boxDefault.FindMinimum()} см");

            Console.WriteLine();

            Console.WriteLine("Введите данные для коробки:");
            int width = ReadPositiveInt("  Ширина (см): ");
            int height = ReadPositiveInt("  Высота (см): ");
            int depth = ReadPositiveInt("  Глубина (см): ");
            Console.Write("  Материал: ");
            string material = Console.ReadLine();

            var box1 = new Box(width, height, depth, material);
            Console.WriteLine($"\nКоробка box1: {box1}");
            Console.WriteLine($"  Объём: {box1.CalculateVolume()} куб.см");
            Console.WriteLine($"  Площадь поверхности: {box1.CalculateSurfaceArea()} кв.см");
            Console.WriteLine($"  Минимальная сторона: {box1.FindMinimum()} см");

            var box2 = new Box(box1);
            Console.WriteLine($"\nКопия box1 (box2): {box2}");
            Console.WriteLine($"  Объём копии: {box2.CalculateVolume()} куб.см");
        }
    }
}