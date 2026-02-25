using Shop_Toshmatovv.Data.Common;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System;

namespace Shop_Toshmatovv.Data.DataBase
{
    public class DBItems : IItems
    {
        private IEnumerable<Categorys> _categories;

        public DBItems()
        {
            // Загружаем категории в конструкторе
            _categories = new DBCategory().AllCategories.ToList();
        }

        public IEnumerable<Items> AllItems
        {
            get
            {
                return GetItemsFromDb();
            }
        }

        public IEnumerable<Items> FindItems(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return AllItems;

            var allItems = GetItemsFromDb();
            return allItems.Where(i =>
                i.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                i.Description.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
            );
        }

        private List<Items> GetItemsFromDb()
        {
            List<Items> items = new List<Items>();

            using (MySqlConnection MySqlConnection = Connection.MySqlOpen())
            {
                MySqlDataReader ItemsData = Connection.MySqlQuery("SELECT * FROM Items ORDER BY `Name`;", MySqlConnection);

                while (ItemsData.Read())
                {
                    // ПОЛУЧАЕМ ID КАТЕГОРИИ (6-я колонка, индекс 5)
                    int categoryId = ItemsData.IsDBNull(5) ? 0 : ItemsData.GetInt32(5);

                    // НАХОДИМ КАТЕГОРИЮ
                    Categorys category = _categories.FirstOrDefault(x => x.Id == categoryId);

                    items.Add(new Items()
                    {
                        Id = ItemsData.IsDBNull(0) ? -1 : ItemsData.GetInt32(0),
                        Name = ItemsData.IsDBNull(1) ? "" : ItemsData.GetString(1),
                        Description = ItemsData.IsDBNull(2) ? "" : ItemsData.GetString(2),
                        Img = ItemsData.IsDBNull(3) ? "" : ItemsData.GetString(3),
                        Price = ItemsData.IsDBNull(4) ? -1 : ItemsData.GetInt32(4),
                        Categorys = category ?? new Categorys() { Id = categoryId, Name = "Неизвестно" }
                    });
                }
            }

            return items;
        }

        public int Add(Items Item)
        {
            using (MySqlConnection MySqlConnection = Connection.MySqlOpen())
            {
                Connection.MySqlQuery(
                    $"INSERT INTO `items`(`Name`, `Description`, `Img`, `Price`, `IdCategory`) VALUES ('{Item.Name}', '{Item.Description}', '{Item.Img}', {Item.Price}, {Item.Categorys.Id});",
                    MySqlConnection);
            }

            int IdItem = -1;

            using (MySqlConnection MySqlConnection = Connection.MySqlOpen())
            {
                MySqlDataReader mySqlDataReaderItem = Connection.MySqlQuery(
                    $"SELECT `Id` FROM `items` WHERE `Name` = '{Item.Name}' AND `Description` = '{Item.Description}' AND `Price` = {Item.Price} AND `IdCategory` = {Item.Categorys.Id};",
                    MySqlConnection);

                if (mySqlDataReaderItem.HasRows)
                {
                    mySqlDataReaderItem.Read();
                    IdItem = mySqlDataReaderItem.GetInt32(0);
                }
            }

            // Обновляем категории после добавления
            _categories = new DBCategory().AllCategories.ToList();

            return IdItem;
        }

        public Items GetItem(int id)
        {
            return AllItems.FirstOrDefault(i => i.Id == id);
        }

        public void Update(Items item)
        {
            using (MySqlConnection connection = Connection.MySqlOpen())
            {
                string query = $"UPDATE Items SET " +
                               $"Name = '{item.Name}', " +
                               $"Description = '{item.Description}', " +
                               $"Img = '{item.Img}', " +
                               $"Price = {item.Price}, " +
                               $"IdCategory = {item.Categorys.Id} " +
                               $"WHERE Id = {item.Id}";

                Connection.MySqlQuery(query, connection);
            }

            // Обновляем категории после изменения
            _categories = new DBCategory().AllCategories.ToList();
        }

        public void Delete(int id)
        {
            using (MySqlConnection connection = Connection.MySqlOpen())
            {
                string query = $"DELETE FROM Items WHERE Id = {id}";
                Connection.MySqlQuery(query, connection);
            }

            // Обновляем категории после удаления
            _categories = new DBCategory().AllCategories.ToList();
        }
    }
}