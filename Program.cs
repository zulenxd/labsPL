using System;
using System.Collections.Generic;

namespace Lab8
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Меню в кафе");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nМеню действий:");
                Console.WriteLine("1. Просмотр всего меню кафе");
                Console.WriteLine("2. Добавить блюдо");
                Console.WriteLine("3. Удалить блюдо по Id");
                Console.WriteLine("4. Запрос: блюда в определенной категории");
                Console.WriteLine("5. Запрос: список вегетарианских блюд");
                Console.WriteLine("6. Запрос: средняя цена всех блюд");
                Console.WriteLine("7. Запрос: количество вегетарианских блюд");
                Console.WriteLine("8. Выйти");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowAllMenuItems();
                        break;
                    case "2":
                        AddNewMenuItem();
                        break;
                    case "3":
                        DeleteMenuItem();
                        break;
                    case "4":
                        QueryByCategory();
                        break;
                    case "5":
                        QueryVegetarianDishes();
                        break;
                    case "6":
                        QueryAveragePrice();
                        break;
                    case "7":
                        QueryVegetarianCount();
                        break;
                    case "8":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }

        private static void ShowAllMenuItems()
        {
            List<MenuItem> items = MenuManager.GetAll();
            if (items.Count == 0)
            {
                Console.WriteLine("Меню пусто.");
                return;
            }
            Console.WriteLine("\n--- Все позиции меню ---");
            foreach (MenuItem m in items)
            {
                Console.WriteLine(m);
            }
        }

        private static void AddNewMenuItem()
        {
            string name;
            string category;
            decimal price;
            bool isVegetarian;

            while (true)
            {
                Console.Write("Введите название блюда: ");
                name = Console.ReadLine();
                try
                {
                    MenuItem.ValidateName(name);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Введите категорию (например, Напитки, Горячее, Десерты): ");
                category = Console.ReadLine();
                try
                {
                    MenuItem.ValidateCategory(category);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Введите цену (руб): ");
                string input = Console.ReadLine();
                if (!decimal.TryParse(input, out price))
                {
                    Console.WriteLine("Ошибка: введите корректное число.");
                    continue;
                }
                try
                {
                    MenuItem.ValidatePrice(price);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Блюдо вегетарианское? (да/нет): ");
                string input = Console.ReadLine().Trim().ToLower();
                if (input == "да")
                {
                    isVegetarian = true;
                    break;
                }
                else if (input == "нет")
                {
                    isVegetarian = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите 'да' или 'нет'.");
                }
            }

            MenuItem newItem = new MenuItem(0, name, category, price, isVegetarian);
            MenuManager.AddMenuItem(newItem);
            Console.WriteLine("Блюдо успешно добавлено в меню.");
        }

        private static void DeleteMenuItem()
        {
            try
            {
                Console.Write("Введите Id блюда для удаления: ");
                int id = int.Parse(Console.ReadLine());
                bool deleted = MenuManager.DeleteMenuItemById(id);
                if (deleted)
                {
                    Console.WriteLine("Блюдо успешно удалено.");
                }
                else
                {
                    Console.WriteLine("Блюдо с таким Id не найдено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        private static void QueryByCategory()
        {
            Console.Write("Введите категорию для поиска: ");
            string category = Console.ReadLine();
            List<MenuItem> result = MenuManager.GetMenuItemsByCategory(category);
            if (result.Count == 0)
            {
                Console.WriteLine("Блюда в данной категории не найдены.");
                return;
            }
            Console.WriteLine($"\n--- Блюда в категории \"{category}\" ---");
            foreach (MenuItem m in result)
            {
                Console.WriteLine(m);
            }
        }

        private static void QueryVegetarianDishes()
        {
            List<MenuItem> result = MenuManager.GetVegetarianDishes();
            if (result.Count == 0)
            {
                Console.WriteLine("Вегетарианских блюд не найдено.");
                return;
            }
            Console.WriteLine("\n--- Вегетарианские блюда ---");
            foreach (MenuItem m in result)
            {
                Console.WriteLine(m);
            }
        }

        private static void QueryAveragePrice()
        {
            decimal avg = MenuManager.GetAveragePrice();
            Console.WriteLine($"\nСредняя цена блюда в кафе: {avg:F2} руб.");
        }

        private static void QueryVegetarianCount()
        {
            int count = MenuManager.GetVegetarianDishesCount();
            Console.WriteLine($"\nОбщее количество вегетарианских блюд: {count}");
        }
    }
}