using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab8
{
    public static class MenuManager
    {
        private static string _filePath = "menu.bin";

        public static void SetFilePath(string path)
        {
            _filePath = path;
        }

        public static void SaveToFile(List<MenuItem> menuItems)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_filePath, FileMode.Create)))
            {
                writer.Write(menuItems.Count);
                foreach (MenuItem item in menuItems)
                {
                    writer.Write(item.Id);
                    writer.Write(item.Name);
                    writer.Write(item.Category);
                    writer.Write(item.Price);
                    writer.Write(item.IsVegetarian);
                }
            }
        }

        public static List<MenuItem> LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new List<MenuItem>();
            }
            List<MenuItem> menuItems = new List<MenuItem>();
            using (BinaryReader reader = new BinaryReader(File.Open(_filePath, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    int id = reader.ReadInt32();
                    string name = reader.ReadString();
                    string category = reader.ReadString();
                    decimal price = reader.ReadDecimal();
                    bool isVegetarian = reader.ReadBoolean(); 

                    menuItems.Add(new MenuItem(id, name, category, price, isVegetarian));
                }
            }
            return menuItems;
        }

        public static List<MenuItem> GetAll()
        {
            return LoadFromFile();
        }

        public static void AddMenuItem(MenuItem item)
        {
            List<MenuItem> menuItems = LoadFromFile();
            int maxId = menuItems.Count == 0 ? 0 : menuItems.Max(m => m.Id);
            item.Id = maxId + 1;
            menuItems.Add(item);
            SaveToFile(menuItems);
        }

        public static bool DeleteMenuItemById(int id)
        {
            List<MenuItem> menuItems = LoadFromFile();
            MenuItem toRemove = menuItems.FirstOrDefault(m => m.Id == id);
            if (toRemove == null)
            {
                return false;
            }
            menuItems.Remove(toRemove);
            SaveToFile(menuItems);
            return true;
        }

        public static List<MenuItem> GetMenuItemsByCategory(string category)
        {
            List<MenuItem> menuItems = LoadFromFile();
            var query = from m in menuItems
                        where m.Category.Equals(category, StringComparison.OrdinalIgnoreCase)
                        select m;
            return query.ToList();
        }

        public static List<MenuItem> GetVegetarianDishes()
        {
            List<MenuItem> menuItems = LoadFromFile();
            var query = menuItems.Where(m => m.IsVegetarian).OrderBy(m => m.Price);
            return query.ToList();
        }

        public static decimal GetAveragePrice()
        {
            List<MenuItem> menuItems = LoadFromFile();
            if (menuItems.Count == 0) return 0;
            return menuItems.Average(m => m.Price);
        }

        public static int GetVegetarianDishesCount()
        {
            List<MenuItem> menuItems = LoadFromFile();
            return menuItems.Count(m => m.IsVegetarian);
        }
    }
}