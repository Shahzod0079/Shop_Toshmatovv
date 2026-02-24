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
        public IEnumerable<Categorys> Categories = new DBCategory().AllCategories;

        public IEnumerable<Items> AllItems
        {
            get
            {
                return GetItemsFromDb();
            }
        }

        // Новый метод для поиска
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
                    items.Add(new Items()
                    {
                        Id = ItemsData.IsDBNull(0) ? -1 : ItemsData.GetInt32(0),
                        Name = ItemsData.IsDBNull(1) ? "" : ItemsData.GetString(1),
                        Description = ItemsData.IsDBNull(2) ? "" : ItemsData.GetString(2),
                        Img = ItemsData.IsDBNull(3) ? "" : ItemsData.GetString(3),
                        Price = ItemsData.IsDBNull(4) ? -1 : ItemsData.GetInt32(4),
                        Categorys = ItemsData.IsDBNull(5) ? null : Categories.FirstOrDefault(x => x.Id == ItemsData.GetInt32(5))
                    });
                }
            }

            return items;
        }
    }
}