using System;

namespace Lab8
{
    public class MenuItem
    {
        private int _id;
        private string _name;
        private string _category;
        private decimal _price;
        private bool _isVegetarian;

        public MenuItem(int id, string name, string category, decimal price, bool isVegetarian)
        {
            _id = id;
            Name = name;
            Category = category;
            Price = price;
            IsVegetarian = isVegetarian;
        }

        public int Id
        {
            get 
            {
                return _id; 
            }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                ValidateName(value);
                _name = value;
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                ValidateCategory(value);
                _category = value;
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                ValidatePrice(value);
                _price = value;
            }
        }

        public bool IsVegetarian
        {
            get { return _isVegetarian; }
            set { _isVegetarian = value; }
        }

        public static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Название не может быть пустым или состоять из пробелов.");
            }
        }

        public static void ValidateCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("Категория не может быть пустой или состоять из пробелов.");
            }
        }

        public static void ValidatePrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentException("Цена не может быть отрицательной.");
            }
            if (price > 1000000)
            {
                throw new ArgumentException("Цена не может превышать 1 000 000 руб.");
            }
        }

        public override string ToString()
        {
            string vegStatus = _isVegetarian ? "Да" : "Нет";
            return $"Id: {_id}, Название: {_name}, Категория: {_category}, Цена: {_price} руб., Вегетарианское: {vegStatus}";
        }
    }
}