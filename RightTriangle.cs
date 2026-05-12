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
}