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
        public int Add(Items Item)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();

            Connection.MySqlQuery(
                $"INSERT INTO `items`(`Name`, `Description`, `Img`, `Price`, `IdCategory`) VALUES ('{Item.Name}', '{Item.Description}', '{Item.Img}', {Item.Price}, {Item.Categorys.Id});",
                MySqlConnection);

            MySqlConnection.Close();

            int IdItem = -1;

            MySqlConnection = Connection.MySqlOpen();

            MySqlDataReader mySqlDataReaderItem = Connection.MySqlQuery(
                $"SELECT `Id` FROM `items` WHERE `Name` = '{Item.Name}' AND `Description` = '{Item.Description}' AND `Price` = {Item.Price} AND `IdCategory` = {Item.Categorys.Id};",
                MySqlConnection);

            if (mySqlDataReaderItem.HasRows)
            {
                mySqlDataReaderItem.Read();
                IdItem = mySqlDataReaderItem.GetInt32(0);
            }

            MySqlConnection.Close();

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
        }

        public void Delete(int id)
        {
            using (MySqlConnection connection = Connection.MySqlOpen())
            {
                string query = $"DELETE FROM Items WHERE Id = {id}";
                Connection.MySqlQuery(query, connection);
            }
        }
    }
}